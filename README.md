# CodeSmellAnnotations

[![.NET](https://github.com/rsvilenov/CodeSmellAnnotations/actions/workflows/dotnet.yml/badge.svg)](https://github.com/rsvilenov/CodeSmellAnnotations/actions/workflows/dotnet.yml) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)  [![nuget](https://img.shields.io/nuget/v/CodeSmellAnnotations)](https://www.nuget.org/packages/CodeSmellAnnotations)

## Annotate your C# codebase with attribute-based code quality remarks, which are picked up by Roslyn.

> netstandard2.0 compliant

### Table of Contents  

- [Objective](#Objective)
- [Usage](#Usage)



### Objective

How many times have we spotted a code smell but didn't have enough time to fix it right away?
Then what do we do? Add a comment, hoping that some day we or somebody else will see this comment and get into fixing the smell? 

Well, that just never happens.

With this library we can add code smell annotations that will appear as warnings in our build as well as in the IDE.
These warnings will bug us and our collegues until someone fixes them.

Key features:
  * Attributes to annotate the code, which get picked by the compiler.
  * A number of predefined common code smells, allowing for quick annotation of bad code.

### Usage

* Chose the most appropriate one of the following attributes:

```csharp
[CodeSmell(Kind.[PredefinedSmellType])]

// or
[CodeSmell(Kind.General, Reason = "unclear intentions")]

// or
[DuplicateOf("MyDoppleganger")]

// or
[DuplicateOf("MyNotExactDoppleganger", Kind = DuplicationKind.OddballSolution)]

// or
[SolidViolation(SolidPrinciple.SingleResponsibility)]
```

* Annotate the code
```csharp
using CodeSmellAnnotations.Attributes;

public class StoreItem
{
    public string Name { get; set; }
    
    [CodeSmell(Kind.PrimitiveObsession, Reason = "use a custom money class with currency info")]
    public decimal Price { get; set; }
}
```

* Get a warning during build:

![image info](./docs/screenshots/shot2.png)

* Navigate to the annotated code by clicking on the warning above:

![image info](./docs/screenshots/shot1.png)
