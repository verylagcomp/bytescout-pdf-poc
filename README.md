# PoC test task

This is a PoC implementation. 
Solution splitted onto 3 projects: _Pdf.Api_, _Pdf.Web_ and _Pdf.DAL_.

# DAL
Just simple library project where _ApplicationDBContext_ can be shared between API and Web projects.

# Web
Signup/sign-in works with out of the box UI.
In-memory database is used for easier development and presentation.

# Api
Has token method. For PoC simplified version token can be retrieved by providing username.
Has simple healthcheck endpoint as default launcher.

# ToDo improvements list
There are many things to improve in current PoC implementation that did not fit in 5 hours of development:
- Add _userManager_ for all operations with users in future
- Add Configuration facade for all settings to make easier retrievement of appsettings in-code (no IConfiguration.GetSection usage)
- Create separate BLL project to store Service logic. Keep controllers code clean and be responsible only for retrieving data, passing data to BLL and sending back the result of service execution
- Add Swagger support for easier API testing
- Add logger for exception logging
