# FreakingAwesome.Data

[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/LICENSE)

This is a forked version of the `Result` and `Result<Ok>` types from [Functional Extensions for
C#](https://github.com/vkhorikov/CSharpFunctionalExtensions) which swap in a list of Validation Errors for the single
string error message found in the original, where each Validation Error consists of an optional list of field names and
error message, similar to the `System.ComponentModel.DataAnnotations` set of functionality. And indeed, you can use Data
Annotations and validate via the `EnsureDataAnnotations()` extension method.

## Motivation

The `Result` structs and extensions are a great foundation, but the single string `Error` inside `Result` and
`Result<Ok>` were too limiting, while `Result<Ok, Err>` extensions were incomplete.

Adding all the extra functionality to `Result<Ok, Err>` just proved too cumbersome and was just to unwieldy in the end,
so I instead chose to swap out the internals of `Result` `Result<Ok>` with an `Error` value I have found useful in the
past, and which allows an easy way to plug in the `System.ComponentModel.DataAnnotations` style of declarative
validation annotations.  Initially I had intended to use a different naming convention than the original `Result`, but
this project grew enough that it is a separate fork, and as such contains mirrors of the `Maybe` types from the
original as well.

## Immutable ValidationErrors

The `ValidationError` and `Result` classes are immutable, but the classes can be joined and chained together in a few
different ways, as in using `Combine()` or `Join()`.

## TODO

 - [ ] Propagate `continueOnCapturedContext` to new async methods (oops)
 - [ ] More tests
 - [ ] Documentation and examples
 - [ ] Publish on nuget.org

## Contributors to the Original [Functional Extensions for C#](https://github.com/vkhorikov/CSharpFunctionalExtensions)

A big thanks to the [Functional Extensions for C#](https://github.com/vkhorikov/CSharpFunctionalExtensions) project contributors!

 * [Vladimir Khorikov](https://github.com/vkhorikov)
 * [Robert Sęk](https://github.com/robosek)
 * [Sergey Solomentsev](https://github.com/SergAtGitHub)
 * [Malcolm J Harwood](https://github.com/mjharwood)
 * [Dragan Stepanovic](https://github.com/dragan-stepanovic)
 * [Ivan Novikov](https://github.com/jonny-novikov)
 * [Denis Molokanov](https://github.com/dmolokanov)
 * [Gerald Wiltse](https://github.com/solvingJ)
 * [yakimovim](https://github.com/yakimovim)
 * [Alex Erygin](https://github.com/alex-erygin)
 * [Omar Aloraini](https://github.com/omaraloraini)
