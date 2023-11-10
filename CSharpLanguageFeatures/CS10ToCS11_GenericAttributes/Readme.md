# Generic Attributes

Based off original document here: https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/generic-attributes.md

## Summary
[summary]: #summary

When generics were introduced in C# 2.0, attribute classes were not allowed to participate. We can make the language more composable by removing (rather, loosening) this restriction. The .NET Core runtime has added support for generic attributes. Now, all that's missing is support for generic attributes in the compiler.

## Motivation
[motivation]: #motivation

Currently attribute authors can take a `System.Type` as a parameter and have users pass a `typeof` expression to provide the attribute with types that it needs. However, outside of analyzers, there's no way for an attribute author to constrain what types are allowed to be passed to an attribute via `typeof`. If attributes could be generic, then attribute authors could use the existing system of type parameter constraints to express the requirements for the types they take as input.

## Detailed design
[design]: #detailed-design

> The direct base class of a class type must not be any of the following types: System.Array, System.Delegate, System.MulticastDelegate, System.Enum, or System.ValueType.

One important note is that the following section of the spec is *unaffected* when referencing the point of usage of an attribute, i.e. within an attribute list: Type parameters - [§8.5](https://github.com/dotnet/csharpstandard/blob/draft-v6/standard/types.md#85-type-parameters).

> A type parameter cannot be used anywhere within an attribute.

This means that when a generic attribute is used, its construction needs to be fully "closed", i.e. not containing any type parameters, which means the following is still disallowed:

```cs
using System;
using System.Collections.Generic;

public class Attr<T1> : Attribute { }

public class Program<T2>
{
    [Attr<T2>] // error
    [Attr<List<T2>>] // error
    void M() { }
}
```

When a generic attribute is used in an attribute list, its type arguments have the same restrictions that `typeof` has on its argument. For example, `[Attr<dynamic>]` is an error. This is because "attribute-dependent" types like `dynamic`, `List<string?>`, `nint`, and so on can't be fully represented in the final IL for an attribute type argument, because there isn't a symbol to "attach" the `DynamicAttribute` or other well-known attribute to.

## Unresolved questions
[unresolved]: #unresolved-questions

- [x] What does `AllowMultiple = false` mean on a generic attribute? If we have `[Attr<string>]` and `[Attr<object>]` both used on a symbol, does that mean "multiple" of the attribute are in use?
    - For now we are inclined to take the more restrictive route here and consider the attribute class's original definition when deciding whether multiple of it have been applied. In other words, `[Attr<string>]` and `[Attr<object>]` applied together is incompatible with `AllowMultiple = false`.
