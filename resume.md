---
---

<img src="/assets/steve.jpg" class="profile" alt="Profile photo"/>

# Steve Desmond

## Summary

- More than a decade of professional full-stack development in .NET, PHP, and JavaScript
- Open source contributions/experience in C, C++, C#, Go, Java, JavaScript, PHP, and Python
- Specializing in performance optimization, automated testing, and Continuous Integration and Delivery 

## Work Experience

### Lead Developer, ecoAPM -- Ithaca, NY (2018 -- Present)

- Building an open source application performance management platform that highlights energy consumption and effiency, to help reduce the environmental burden that software has on the world
- Assisting organizations with any software development needs; focusing on performance, testing, and code quality

### Lead Developer, Steve Desmond Software Development -- Ithaca, NY (2016 -- 2020)

- Solved clients' business needs locally and remotely with well-crafted software
- Created new sites, apps, and APIs; built features and fixing defects in existing applications
- Refactored code to improve maintainability, added automated test coverage to ensure functionality was preserved
- Profiled and optimed client-side, server-side, and database performance
- Provided training to team members on modern application architecture, development and delivery best practices
- Configured continuous integration environments and set up build pipelines to enable continuous delivery

### Enterprise Application Developer, Ithaca College -- Ithaca, NY (2013 -- 2016)

*Accomplishments*

- Led implementation of several modern development processes and practices:
  - Application containers (Docker)
  - Continuous Integration and Delivery (Go.CD)
  - Automated cloud provisioning (Azure CLI)
  - Application Performance Monitoring (New Relic)
  - Configuration management (Ansible)
  - Source control and code collaboration (GitLab)
- Modernized student self-service site (previously updated in 2005) with a [new mobile-first responsive design](http://theithacan.org/news/ithaca-college-to-release-homerconnect-2-0/)
- Optimized memory utilization and improved architecture to provide up to 300x performance improvements
- Created lightweight automated testing and JSON parsing frameworks for Oracle PL/SQL

*Day-to-day*

- Developed new features and fixed defects for public-facing and campus-community web applications
- Maintained dozens of legacy applications, ranging widely in size and complexity
- Added test coverage and refactored legacy code, increasing confidence and decreasing cost of continued support
- Created RESTful web services to act as secure APIs between front-end applications and back-end systems
- Built internal libraries and frameworks to increase efficiency and consistency across projects and applications
- Worked directly with users and managers to develop requirements and prioritize feedback

### Software Developer / System Administrator, Performance Systems Development -- Ithaca, NY (2010 -- 2013)

*Accomplishments*

- Created a configurable workflow engine, allowing customers to customize how jobs are processed
- Created an extensible plugin framework, allowing customer-specific modules to be added dynamically
- Expanded the use of the Continuous Integration environment (CruiseControl.NET) to:
  - Automate the release and deployment of applications to QA, staging, and production environments
  - Automatically apply configuration and customization to dozens of customer-specific deployments
- Increased automated unit test code coverage of core assemblies to 100%

*Day-to-day*

- Developed desktop and web-based applications to model and track energy efficiency upgrades
- Built visual reports to aggregate building attributes and performance results across thousands of entities
- Profiled application and database performance, improving the speed of the application by as much as 120x
- Employed a custom Enterprise Service Bus to handle long-running back-end processes asynchronously
- Monitored and optimized virtual resource allocation, performed system administration duties
- Took on many product owner and ScrumMaster responsibilities, including:
  - Facilitating iteration planning and retrospective meetings
  - Transforming customer requests and functional specifications into user stories
  - Analyzing sprint progress and reporting release date estimates to management
  - Project management tool administration

### Programmer/Analyst / Investment Support, Loews Corporation -- New York, NY (2007 -- 2010)

- Developed desktop and web-based applications connected to back-end trading systems
- Deployed applications and system updates (both in-house and vendor supplied)
- Built customized, graphical reports on the company's investments and financial transactions
- Created user documentation for system setup and deployment, and specialized login/security practices
- Provided Linux expertise in an otherwise Windows-only environment
- Technical support contact for more than 100 Investment department users and executives

### Software Developer / Assistant System Administrator, Bartek Ingredients -- Stoney Creek, ON (2006 -- 2007)

- Developed and deployed custom order processing, shipping and tracking, and safety compliance applications

## Conference Presentations

{% for talk in site.data.talks %}

### {{talk.title}}{% if talk.subtitle %}: {{talk.subtitle}}{% endif %}

#### {{talk.event}} -- {{talk.location}} ({{talk.date | date: '%B %-d, %Y'}})

{% for item in talk.description %}
- {{item}}{% endfor %}

{% endfor %}

See [https://stevedesmond.ca/talks](/talks) for slides and video recordings of presentations

## Education

### Ithaca College, 2003 -- 2007 (graduated Cum Laude)

#### Bachelor of Science, Computer Science