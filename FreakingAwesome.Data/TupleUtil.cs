using System.Threading.Tasks;

namespace FreakingAwesome.Data
{
    public static class TupleUtil
    {
        public static async Task<(T1, T2)> CreateAsync<T1, T2>(Task<T1> task1, Task<T2> task2)
        {
            await Task.WhenAll(task1, task2);
            return (task1.Result, task2.Result);
        }

        public static async Task<(T1, T2, T3)> CreateAsync<T1, T2, T3>(Task<T1> task1, Task<T2> task2, Task<T3> task3)
        {
            await Task.WhenAll(task1, task2, task3);
            return (task1.Result, task2.Result, task3.Result);
        }

        public static async Task<(T1, T2, T3, T4)> CreateAsync<T1, T2, T3, T4>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4)
        {
            await Task.WhenAll(task1, task2, task3, task4);
            return (task1.Result, task2.Result, task3.Result, task4.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5)> CreateAsync<T1, T2, T3, T4, T5>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6)> CreateAsync<T1, T2, T3, T4, T5, T6>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7)> CreateAsync<T1, T2, T3, T4, T5, T6, T7>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15, Task<T16> task16)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15, task16);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result, task16.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15, Task<T16> task16, Task<T17> task17)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15, task16, task17);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result, task16.Result, task17.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15, Task<T16> task16, Task<T17> task17, Task<T18> task18)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15, task16, task17, task18);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result, task16.Result, task17.Result, task18.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15, Task<T16> task16, Task<T17> task17, Task<T18> task18, Task<T19> task19)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15, task16, task17, task18, task19);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result, task16.Result, task17.Result, task18.Result, task19.Result);
        }

        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20)> CreateAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8, Task<T9> task9, Task<T10> task10, Task<T11> task11, Task<T12> task12, Task<T13> task13, Task<T14> task14, Task<T15> task15, Task<T16> task16, Task<T17> task17, Task<T18> task18, Task<T19> task19, Task<T20> task20)
        {
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14, task15, task16, task17, task18, task19, task20);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result, task11.Result, task12.Result, task13.Result, task14.Result, task15.Result, task16.Result, task17.Result, task18.Result, task19.Result, task20.Result);
        }
    }
}
