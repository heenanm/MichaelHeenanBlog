- Use the following:
  - [ASP.NET Core 3.0](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0) - cross-platform, high-performance, open-source framework for building modern, cloud-based, Internet-connected applications (the replacement for the [.NET Framework](https://en.wikipedia.org/wiki/.NET_Framework))
  - [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-3.0) - one of the ways to create ASP.NET Core websites (the other being [MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-3.0))
  - [Microsoft SQL Server](https://www.microsoft.com/en-in/sql-server/sql-server-2019) - relational database
  - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - library for interacting with databases (including Microsoft SQL Server)
  - [Bootstrap](https://getbootstrap.com/) - front-end component library (prettifies websites)
  - [jQuery](https://jquery.com/) - abstraction library that makes it easier to interface with websites using JavaScript
  - [Serilog](https://serilog.net/) - library for creating structured logs
  - [Seq](https://datalust.co/) - web portal where logs are stored
  - [Azure DevOps](https://azure.microsoft.com/en-gb/services/devops/)
- Create an area where anyone can browse public information
  - List the blog posts on the homepage
    - Ordered by the date when they were created (the most recent first)
    - Add pagination so that a maximum 10 blog posts are displayed on each page
  - View a single blog post
    - Display the title
    - Display the date when it was created
    - Display the amount of time it'll take to read
    - Display the tags (if any)
    - Display the body
  - Share a blog post on Twitter using a predefined message
  - View existing comments on a blog post
  - Create a new comment on a blog post
  - View a tag cloud
    - Display each tag along with the respective number of blog posts
  - List the blog posts by a tag
    - Ordered by the date when they were created (the most recent first)
    - Add pagination so that a maximum 10 blog posts are displayed on each page
- Create an area where only authenticated users can access private information
  - Sign in
    - Create at least two users who can access the private area
    - Ensure that passwords are stored securely
  - Sign out
  - List the blog posts in a tabular format
  - Search for a blog post
  - Create a blog post
  - Update a blog post
  - Delete a blog post
    - Prompt for confirmation
  - View the comments for a blog post
  - Delete a comment for a blog post
- Add [Markdown](https://daringfireball.net/projects/markdown/) support to blog posts
- Deploy the blog to Azure
  - Utilise a personal Subscription
    - App Service Plan
    - App Service
    - Azure SQL
  - Ensure all of the application settings are configured within the App Service (e.g. Seq URL, SQL Server connection string, etc)

## Expectations

- Demonstrate a good understanding of web technologies
- Demonstrate that performance has been considered when making certain decisions (e.g. database communication)
- The requirements are satisified without over-engineering the solution
- NO form of automated tests

## Tips

- [Keep It Simple Stupid](https://en.wikipedia.org/wiki/KISS_principle)