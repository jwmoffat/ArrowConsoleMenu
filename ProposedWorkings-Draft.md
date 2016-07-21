# Original Purpose

Originally I started this code to be a method of allowing a user, particularly a person in a QA department, to browse through various commands within an 
app.   It would give them a list of options, execute the command they wanted, then return them back to the menu again.

Over time, this has slowly changed, including this document.  Hopefully this is starting to make more sense.

## Overview

Each Menu has a list of options.  Each option can do one of two general actions:
1) Run a custom command
2) Pick a value from a list

Choosing an item is done by using the arrow keys:
- Up/Down select the item
- Right chooses that menu option
- Left exits the current menu

## Beginning Menu

A very basic menu might be just a list options to run various pieces of code.   For example:

```
----------------------------
 My Menu
----------------------------
 > 1. Print 'Hello World'
   2. Show current date
   3. Send me an email
----------------------------
```

Some of these commands you might want the user to wait for the user to read what's been output, so you can specify an additional wait flag for commands.
To create a menu like this is very straight-forward:

```csharp
static void Main(string[] args)
{
   var menu = new Menu("My Menu");
   menu.AddCommand("Print 'Hello World'", () => { Console.WriteLine("Hello World"); }, wait: true);
   menu.AddCommand("Show current date",   () => { Console.WriteLine(DateTime.Now);  }, wait: true);
   menu.AddCommand("Send me an email",    () => { Emailer.SendEmail(); });							  // wait = false by default
   menu.Show();   
}
```

## Setting a value using menus

The next step of the menu is to allow addition of an option where the user can select a value.   For example:

```
----------------------------
 My Menu
----------------------------
 > 1. Set Name [Adam]
   2. Print Selected Name
----------------------------
```

For this example, the first option lets the user select the name from a list.  If the user chooses the Set Name option, they see:
```
----------------------------
 Set Name
----------------------------
 > 1. Adam
   2. Bob
   3. Sally
----------------------------
```

If the user chose option 2 (Bob), the menu reverts back to:
```
----------------------------
 My Menu
----------------------------
 > 1. Set Name [Bob]
   2. Print Selected Name
----------------------------
```

That's all good, but now how does that work for passing the value to print it?   The answer is to reference the menu option was added.

```csharp
static void Main(string[] args)
{
   var setNameChoice = new MenuChoices("Set Name", new List<string> { "Adam", "Bob", "Sally" });      
   var menu = new Menu("My Menu");  
   menu.AddChoices(setNameChoice);
   menu.AddCommand("Print Selected Name", () => { Console.WriteLine($"Current name = {setNameChoice.SelectedItem}"); });
   menu.Show();   
}
```

## Setting a value, but list is based off another option

While maybe not standard, a new feature of the menu was wanted.  This example differs from the others slightly, mostly for the sake of simplicity:

```
----------------------------
 My Menu
----------------------------
 > 1. Set Country [Canada]
   2. Set Local Region [BC]
   3. Print Current Region
----------------------------
```

In this case, each country will have their own respective sub-choices.  Programming this could look like:

```csharp
static void Main(string[] args)
{
   var setCountryChoice = new MenuChoices("Set Country", new List<string> { "Canada", "USA" });
   var setLocalRegionChoice = new MenuChoices("Set Local Region", () => { GetLocalRegionsFor(setCountryChoice.SelectedItem); });

   var menu = new Menu("My Menu");  
   menu.AddChoices(setCountryChoice);
   menu.AddCommand("PPrint Current Region", () => { Console.WriteLine($"Current region = {setLocalRegionChoice.SelectedItem}"); });
   menu.Show();   
}

List<string> GetLocalRegionsFor(string country)
{
   switch (country)
   {
      case "Canada": 
	     return new List<string> { "BC", "AB" };    // etc
	  case "USA": 
	     return new List<string> { "AB", "WA" };    // etc..
	  default:
	     throw new NotImplementedException();
   }
}
```


## Non-String Lists

It makes sense that the list of items being passed in don't have to be strings.  There's only so much viability from doing this.  For
example, you could want to run a function against a database for one option, and another option to select the given database.   

```csharp
static void Main(string[] args)
{
   // NB: Database class overrides ToString() so menu items are identifiable

   var databaseChoices = new MenuChoices<Database>("Set Selected Database", GetAllDatabases());
   
   var menu = new Menu("My Menu");  
   menu.AddChoices(setCountryChoice);
   menu.AddCommand<Database>("Run Report Against Selected Database", (currDatabase) => { RunReport(currDatabase); });
   menu.Show();   
}

void RunReport(Database currDatabase)
{
   // Do stuff
}
```

The menu for this might appear as
```
----------------------------
 My Menu
----------------------------
 > 1. Set Selected Database [Northwind]
   2. Run Report Against Selected Database
----------------------------
```

## Setting a value and running a command

Currently, this feature is not yet implemented.
