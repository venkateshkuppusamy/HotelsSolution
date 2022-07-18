How to Run the app
The Hotel solution has two paths.
1. UI - Available at Source/UI/HotelAngularApp 
2. Backend - Available at Source/Backend/HotelSolution.sln

To run the app - 
Opne the commandline, go to HotelSolution.sln path and build solution using command dotnet build. Then run the project using the command dotnet run --project HotelsAPI/HotelsAPI.csproj
Open commandline, go to HotelAngularApp path and type ng serve. Then angular app should run at http://localhost:4200/hotels

Open commandline, go to root\Source\Backend path and build solution using command "dotnet build". Then run the project using the command "dotnet run --project HotelsAPI/HotelsAPI.csproj"
The api should be running at https://localhost:7233. The swagger link is available at "https://localhost:7233/swagger/index.html" 
Open commandline, go to root\Source\UI\HotelAngularApp path and type "ng serve". Then angular app should run at Hotels app


Q&A Section
1. How long did you spend on the coding test and how would you improve your solution if you had more time? (If you were unable to spend as much time as you would have liked on the coding test, use your answer as an opportunity to explain what you would add).
	The solution was build in 20 hours. 8 hours for UI and 12 hours for backend api.
	What could be improved on this, solution
		1. Add more details to the swagger api documentation
		2. Implement logging usually serilog.
		3. Reterive data from database.
		4. Implement caching in service layer.
		5. Improve on the UI theme and responsiveness.
		
2. Describe the tooling / libraries / packages you chose to use for your development process and the reasons why.
	1. Backend tools 
		Framework - .net core 6 - Latest stable .net core version.
		Unit test - xunit		- open source framework. better community support.
		Assertion - Fluent assetions - Readable assetions apis.
		Validation - Fluent validations. - Seperate API data models and validation rules.
		Mocking	- moq	- Better mocking framework that microsoft inbuilt provided.
		Json Serializer - Newtonsoft json. - Fast Json serialized with advance support to JSON manipulation via JToken and Json path.
	2. Front end tools
		Angular 14		- Latest stable version
		Angular material - Continuous support for all angular version
		Visual studio code -Light weight tool for angular development and good extensions available.
	
3. Describe how this solution would be deployed and run in your chosen cloud provider and any impact this may have on its development.
	Frontend UI and Backend API can be deployed in Azure as two seperate app services mostly close to the customer regions for security and better performance.
	Also we can consider an option of hosting Angular in storage account as a static site.
	
4. If the applications was enhanced to contain business sensitive data what considerations and possible solutions would you consider for securing it?
	When it comes of security of the data, the data should be secured both at rest and in transit.
	1. In transit - Use https protocol for both api and angular site.
					Application security can be provide use OpenID and OAuth protocol(Azure AD and RBAC).
					If not public facing app, but used by customer service representation additionaly the side can be hosted in the Azure VNET connected back to org's network using VPN.
					Enable ddos protection on apps.
	2. At rest 	  - Deploy apps and database in regions compliant to customers data regions compliant to Country's data compliance.
	 				Azure sql data and storage account data are encrypted by default. We can use custome key if default encryption key is not sufficient.
					Additonal data masking or encryption using SQL inbuilt en features.

5. How would you track down a performance issue in production and what was your last experience of this?
	1. Collect application insights to understand the performance of the underlying resourses where the applications are hosted.
	2. Use Azure status check if any of the azure resources/services are degraded at region level.
	3. Collect recent change activity on application code, db configuration, infra and changes/upgrades.
	4. Collect ticket information to see if this is user specific or happens to all users.
	5. If availability test is configured use collect that information as well.
	6. Based on the above data, we can corner the performance issue to application, db or infra and further drill down to identify the cause.

Monitoring is the key to track performace issue. The last project that I worked on build on Azure. The team could leverage Azure monitor and application insights
to track performance issue.

