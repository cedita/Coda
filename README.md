# Coda
Coda provides a base set of extension methods, libraries and interfaces to implement in client applications. As a .NET Standard library, it works everywhere that you may wish to use it.

It used used by [Cedita](https://www.cedita.com/) for assisting in the building of enterprise-level applications.

## Feature Focus
### Repository Pattern
Whilst the Repository pattern can be frowned upon in some circles, it's still a useful abstraction to make available. Found within `Coda.Data` is `IRepository<TData, TKey>` which can be used to build your repositories on top of that will follow a common interface, with both synchronous and asynchronous methods.

~`Coda.Data.EntityFramework` and `Coda.Data.EntityFrameworkCore` provide base repositories for EF and EF Core respectively.~ (At time of writing these libraries are not yet ported)

### Display Attributes from Enums
When working with enums it's nice to be able to seamlessly read the DataAnnotations Display attribute.

    public enum MyEnum
    {
        [Display(Name = "Default Value")]
        DefaultValue
    }
    // ..
    using Coda.Extensions;
    // ..
    MyEnum.DefaultValue.GetDisplayName(); // "Default Value"

### String Extensions
Use more natural syntax for string operations such as:

    using Coda.Extensions;
    // ..
    var myString = "Really long string to work with.";
    myString.IsNullOrEmpty(); // false
    myString.IsNullOrWhitespace(); // false
    myString.MaxLength(12, "..."); // "Really lo..."
    
### Easier Model Copying
Copying values from one instance to another is simplified with `ValuesFrom<T>` extensions.

    using Coda.Extensions;
    // ..
    public class MyObject
    {
       public string TestString { get; set; }
       public int TestInt { get; set; }
    }
    // ..
    var baseObj = new MyObject { TestString = "Hello World", TestInt = 0 };
    var dbObj = new MyObject { TestString = "Goodbye World", TestInt = 1 };
    dbObj.ValuesFrom(baseObj, nameof(MyObject.TestString)); // { TestString = "Hello World", TestInt = 1 }
    // or, for same result using exclusion:
    dbObj.ValuesFrom(baseObj, isExclusionList: true, properties: nameof(MyObject.TestInt));
