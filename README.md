# FreakingAwesome.ValidationResult

This is a forked and slimmed down version of the `Result` and `Result<T>` types from [Functional Extensions for
C#](https://github.com/vkhorikov/CSharpFunctionalExtensions) which swap in a list of Validation Errors for the single
string error message found in the original, where each Validation Error consists of an optional field name and error
message, reminiscent of ASP.Net MVC ModelState error reporting paradigm.

[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/LICENSE)

## Immutable ValidationErrors

The `ValidationError` and `ValidationResult` classes are immutable, but the classes can be joined and chained together
in a few different ways.

## TODO

 - [ ] More tests
 - [ ] Documentation and examples
 - [ ] Publish on nuget.org

## [Functional Extensions for C#](https://github.com/vkhorikov/CSharpFunctionalExtensions) Contributors

A big thanks to the project contributors!

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
