Created from https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio

each service retrieves encrypted data (password) then calls the api directly from the other service. This service passes the other services everything it needs to send the password

todo clean up code, lot of it is imported, probably don't want a lot of it yngni

infersharp static code analyzer

Add analytics to reddit bot and other services.
copy features from linktree

https://learn.microsoft.com/en-us/aspnet/core/security/?WT.mc_id=dotnet-35129-website&view=aspnetcore-7.0

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-7.0&tabs=visual-studio

Safely link accounts with https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-7.0&tabs=visual-studio  BIG TODO HERE
Ones that don't support this, or not well enough, we need to store the acc. Also, maybe refactor the database for this. Account_automation table may need to change

add analytics and logging https://vmsdurano.com/serilog-and-asp-net-core-split-log-data-using-filterexpression/ and firebase? 

docs for db https://www.npgsql.org/efcore/mapping/general.html?tabs=data-annotations
https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations
search google for 'ef.core convert property dbset'
https://learn.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#foreign-key
https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting
https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes
Search for EF7 stuff and youll find it fluent api

multiple devices are supported for session use. You can use the same session across multiple devices

TODO: redis for login sessions
TODO: https://old.reddit.com/prefs/apps/ works with the redirect bug


	continue testing with scheduling... then do failure testing. Make sure the front end does it right
	
	todo make generic deep copy function. take it out of the test files where it serializes and deserializes


Planning notes:
when planning the next integration, it is insanely benefitial to rely on 3rd party libraries
to maintain the connection to the api. This way I have way less to do for breaking changes.

need to setup permissions for recent functionality

base job service, some code will get duplicated. Focused on how the functions will get extended. If there's too much functionality with the type properties, just leave them separated. Some are good candidates for base class. Once I have the engagement tested
break out the automation job service and actually test that and see that the scheduled jobs still run

get good test coverage on the changes.
Also add permissions for this stuff!! premium not premium etc etc. Probably have 3 plans free, mid, and deluxe. rule of 3 but support custom pricing if needed

Setup DefaultDataService, will set default data on startup. Types, default rules, etc etc.

validate all database column lengths, engagementrule wasn't validating

see if it would optimize calls to the reddit service by batching together the calls... If 1 call fails, more data is lost. and some calls might be big

TODO: all try catch exception has logging in it. Also test the app with all exceptions breaking the app to debug any exceptions that may happen
ALSO put try catch in ALLL data access objects and log the error from the try catch


https://praw.readthedocs.io/en/latest/package_info/praw7_migration.html

TODO: address all ctrl+F TODOs