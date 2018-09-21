using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ResultTests
{
    public class SerializationTests
    {
        private static readonly string _errorMessage = "this failed";

        [Fact]
        public void GetObjectData_sets_correct_statuses_on_success_result()
        {
            Result okResult = Result.Ok();
            ISerializable serializableObject = okResult;

            var serializationInfo = new SerializationInfo(typeof(Result), new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            serializationInfo.GetBoolean(nameof(Result.IsSuccess)).Should().BeTrue();
            serializationInfo.GetBoolean(nameof(Result.IsFailure)).Should().BeFalse();
        }

        [Fact]
        public void GetObjectData_sets_correct_statuses_on_failure_result()
        {
            Result okResult = Result.Fail(_errorMessage);
            ISerializable serializableObject = okResult;

            var serializationInfo = new SerializationInfo(typeof(Result), new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            serializationInfo.GetBoolean(nameof(Result.IsSuccess)).Should().BeFalse();
            serializationInfo.GetBoolean(nameof(Result.IsFailure)).Should().BeTrue();
        }

        [Fact]
        public void GetObjectData_adds_message_in_context_on_failure_result()
        {
            Result okResult = Result.Fail(_errorMessage);
            ISerializable serializableObject = okResult;

            var serializationInfo = new SerializationInfo(typeof(Result), new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            serializationInfo.GetValue(nameof(Result.Error), typeof(ValidationError[]))
                .ShouldBeEquivalentTo(new ValidationError[] { new ValidationError(_errorMessage) });
        }

        [Fact]
        public void GetObjectData_of_generic_result_adds_object_in_context_when_success_result()
        {
            SerializationTestObject language = new SerializationTestObject { Number = 232, String = "C#" };
            Result<SerializationTestObject> okResult = Result.Ok(language);
            ISerializable serializableObject = okResult;

            var serializationInfo = new SerializationInfo(typeof(Result), new FormatterConverter());
            serializableObject.GetObjectData(serializationInfo, new StreamingContext());

            serializationInfo.GetValue(nameof(Result<SerializationTestObject>.Value), typeof(SerializationTestObject))
                .Should().Be(language);
        }

        [Fact]
        public void BinarySerialization_gives_back_a_resembling_success_result()
        {
            Result okResult = Result.Ok();
            using (MemoryStream stream = SerializeToStream(okResult))
            {
                var actual = (Result)DeserializeFromStream(stream);

                actual.IsSuccess.Should().BeTrue();
                actual.IsFailure.Should().BeFalse();
            }
        }

        [Fact]
        public void BinarySerialization_gives_back_a_resembling_failure_result()
        {
            Result okResult = Result.Fail(_errorMessage);
            using (MemoryStream stream = SerializeToStream(okResult))
            {
                var actual = (Result)DeserializeFromStream(stream);

                actual.IsSuccess.Should().BeFalse();
                actual.IsFailure.Should().BeTrue();
                actual.Error.Single().ShouldBeEquivalentTo(new ValidationError(_errorMessage));
            }
        }

        [Fact]
        public void BinarySerialization_generic_gives_back_a_resembling_success_result()
        {
            SerializationTestObject language = new SerializationTestObject { Number = 232, String = "C#" };
            Result<SerializationTestObject> okResult = Result.Ok(language);
            using (MemoryStream stream = SerializeToStream(okResult))
            {
                var actual = (Result<SerializationTestObject>)DeserializeFromStream(stream);

                actual.IsSuccess.Should().BeTrue();
                actual.IsFailure.Should().BeFalse();
                actual.Value.ShouldBeEquivalentTo(language);
            }
        }

        [Fact]
        public void BinarySerialization_generic_gives_back_a_resembling_failure_result()
        {
            SerializationTestObject language = new SerializationTestObject { Number = 232, String = "C#" };
            Result<SerializationTestObject> okResult = Result.Fail<SerializationTestObject>(_errorMessage);
            using (MemoryStream stream = SerializeToStream(okResult))
            {
                var actual = (Result<SerializationTestObject>)DeserializeFromStream(stream);

                actual.IsSuccess.Should().BeFalse();
                actual.IsFailure.Should().BeTrue();
                actual.Error.Single().ShouldBeEquivalentTo(new ValidationError(_errorMessage));
            }
        }

        static MemoryStream SerializeToStream(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        static object DeserializeFromStream(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object o = formatter.Deserialize(stream);
            return o;
        }
    }

    [Serializable]
    public class SerializationTestObject
    {
        public string String { get; set; }
        public int Number { get; set; }
    }
}
