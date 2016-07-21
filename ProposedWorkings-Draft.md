# Original Purpose

Originally I started this code to be a method of allowing a user, particularly a person in a QA department, to browse through various commands within an app.   It would give them a list of options, execute the command they wanted, then return them back to the menu again.

The code was to be fairly straight-forward in terms of linking up a console project against a library that was under test.

## Key Features

After some initial work, a few key features arose:
* listed commands to run with a description
* method of changing parameters used for a command (ie: current database)
* using arrow keys to navigate through menu 
* sub-menus to keep parent menus simpler

Things that are back-burnered for the next iteration:
* letting menus interact with each other (parents, children, siblings, etc), ie: settings are not explicitly shared between menus.  (feel free to use static variables if you want)

Other items that aren't clarified yet:
* what to do after executing a command (press ENTER/Backspace/LeftArrow to return to menu?)
* figure out what else I'm missing

Given a menu structure, the class will print out the list of items, without anything expanded by default.  
* Items that end with ```...``` indicate a sub-menu that will expand in place.   
* Items with square brackets, such as ```[Some Name]```, are for setting values within the menu, such as the currently selected database (in these examples).

# Example Menu Layout (Prototyping)

This is definitely a **work in progress** and changes as I create it.

Initial layout (collapsed), marker on item 2:
 ```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Test Email ...
 > 2) Show Status ...
   3) Last Notified ...
   4) Debug Options ...
   5) Print 'Hello World'
```

Initial layout (expanded sub-menu 2):
 ```
-----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Test Email ...
 > 2) Show Status
     a) Set Database [Client 1]
	 b) Run Debug Status
   3) Last Notified ...
   4) Debug Options ...
   5) Print 'Hello World'
```

Initial layout (all items expanded; **shows structure**):
 ```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Test Email
 > 2) Show Status
     a) Set Database [Client 1]
	 b) Run Debug Status
   3) Last Notified
     a) Change Site [Site 3]
 	 b) Show Last Notified
   4) Debug Options
 	 a) Statistics
	      i) Last Ran
		 ii) Time to Execute
	 b) Change settings
	      i) Debug Level [WARNING]
		 ii) Default Email [testEmail@localhost]
	    iii) Run times [30 min]
         iv) Save debug settings
   5) Print 'Hello World'
```

## Adjusting a SetValueEntry:

Let's say expanded 'Show Status' and are to change the current database.

 ```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Test Email
   2) Show Status
 >   a) Set Database [Client 1]
	 b) Run Debug Status
   3) Last Notified
   4) Debug Options
   5) Print 'Hello World'
```

Pressing right-arrow (or enter), gives a new list/menu appears, with previous selected item set:
 ```
 -----------------------------------------------
   Show Status -> Set Database [Client 1]
 -----------------------------------------------
 > 1) Client 1
   2) Client 2
   3) Some Other Client
   4) Yet Another Client
```

Choosing enter on 3 would set the value and return to the previous menu state:
```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Test Email
   2) Show Status
 >   a) Set Database [Some Other Client]
	 b) Run Debug Status
   3) Last Notified
   4) Debug Options
   5) Print 'Hello World'
```

## Dependent SetValueEntries

Consider the following case:
```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Choose Car Maker [Ford]
   2) Choose Model [F-150]
```

In this case, changing the selected entry for the first menu affects the list of items in the second menu.   So changing
Car Maker to Dodge would give
```
 -----------------------------------------------
   Main Menu
 -----------------------------------------------
   1) Choose Car Maker [Dodge]
   2) Choose Model [Ram 1500]
```

(*This will likely change the formatting of other parts of the document*)

## SubMenus

Submenus should be treated the same as any regular menu.  In fact, the code that generates a submenu should be stand-alone if wanted.   ie: say the main menu template, that generates previous menu examples above, is something like:

```csharp
 var menu = new Menu("Main Menu");  
 menu.AddCommand("Test Email", () => { /* code to send test email */ });
 menu.AddMenu(GetShowStatusMenu());
 // etc..
  
 menu.Show();
```

Then this should also work:

```csharp
 var menu = GetShowStatusMenu();  
 menu.Show();
```

giving the output like:
```
-----------------------------------------------
  Show Status
-----------------------------------------------
 >  1) Set Database [Some Other Client]
    2) Run Debug Status
```


## Implementing menu:

I'm not happy with the current state, but here's my first draft of how code to generate this menu **MIGHT** look like:

 ```csharp
static void Main(string[] args)
{
    var menu = new Menu("Main Menu");

    menu.AddCommand("Test Email", () => { /* code to send test email */ });
    menu.AddMenu(GetShowStatusMenu());
    menu.AddMenu(GetLastNotifiedMenu());
    menu.AddMenu(GetDebugOptionsMenu());
    menu.AddCommand("Print 'Hello World'", () => { Console.WriteLine("Hello World"); });

    menu.Show();
}

public static Menu GetShowStatusMenu()
{
    List<Database> listOfDatabaseObjects = GetMyDatabases();                       // uses .ToString() to determine text to display in menu

    var statusMenu = new Menu("Show Status");
           
    statusMenu.AddSetVariableCommand("Set Database", listOfDatabaseObjects);
    statusMenu.AddCommand<Database>("Run Debug Status", DebugStatusFunction);      // essentially runs "DebugStatusFunction( listOfDatabaseObjects[selectedIndex] )"
            
    return statusMenu;
}

public static Menu GetLastNotifiedMenu()
{
    List<string> listOfSiteObjects = new List<string> {"Site 1", "Site 2"};

    var notificationMenu = new Menu("Last Notified");

    notificationMenu.AddSetVariableCommand("Change Site", listOfSiteObjects);
    notificationMenu.AddCommand<string>("Show Last Notified", ShowLastNotifiedForSite);

    return notificationMenu;
}

public static Menu GetDebugOptionsMenu()
{
    var debugOptionsMenu = new Menu("Debug Options");   

    var statisticsSubMenu = new Menu("Statistics");
    statisticsSubMenu.AddCommand("Last Ran", () => { /* code */ });
    statisticsSubMenu.AddCommand("Time to execute", () => { /* code */ });

    var changeSettingsSubMenu = new Menu("Change settings");
    changeSettingsSubMenu.AddSetVariableCommand("Debug Level", GetDebugLevels());
    changeSettingsSubMenu.AddSetStringValueCommand("Default Email", ref defaultEmail);
    changeSettingsSubMenu.AddSetVariableCommand("Run times", GetAllowedRunTimes());
    changeSettingsSubMenu.AddCommand("Save debug settings", SaveDebugSettings);

    debugOptionsMenu.AddMenu(statisticsSubMenu);
    debugOptionsMenu.AddMenu(changeSettingsSubMenu);

    return debugOptionsMenu;
}
 ```
