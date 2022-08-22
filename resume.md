---
title: Résumé
---

<img src="/assets/steve.jpg" class="profile" alt="Profile photo"/>

# Steve&nbsp;Desmond

## Summary

- {{ 'now' | date: "%Y" | minus: 2005 }} years of professional full-stack development
- Open source contributions / experience in Bash, C, C++, C#, Docker, F#, Go, Java, JavaScript, PHP, Python, TypeScript, and Visual Basic
- Specializing in performance optimization, user experience, automated testing, and Continuous Integration and Delivery

## Work Experience

### Lead Developer, ecoAPM -- Ithaca, NY (2018 -- Present)

- Building an open source Application Performance Management platform that highlights energy consumption and efficiency, to help reduce the environmental toll that software takes on the planet
- Assisting organizations with any and all software development needs, focusing on performance, UX, testing, and code quality
- Offering Product Owner and Project Management services to help teams build high quality features from inception to deployment, specializing in [inclusive design](https://corgibytes.com/blog/2020/12/08/inclusive-design/) and [data-driven UX improvements](https://corgibytes.com/blog/2021/04/13/data-driven-ux/)

### Lead Developer, Steve Desmond Software Development -- Ithaca, NY (2016 -- 2020)

- Solved clients' business needs locally and remotely with well-crafted software
- Created new sites, apps, and APIs; built features and fixing defects in existing applications
- Refactored code to improve maintainability, added automated test coverage to ensure functionality was preserved
- Profiled and optimized client-side, server-side, and database performance
- Provided training to team members on modern application architecture, development and delivery best practices
- Configured continuous integration environments and set up build pipelines to enable continuous delivery

### Enterprise Application Developer, Ithaca College -- Ithaca, NY (2013 -- 2016)

- Led implementation of several modern development processes and practices: containers, CI/CD, cloud, APM, config mgmt
- Modernized student self-service site (previously updated in 2005) with a [new mobile-first responsive design](http://theithacan.org/news/ithaca-college-to-release-homerconnect-2-0/)
- Optimized memory utilization and improved architecture to provide up to 300x performance improvements
- Implemented major upgrades and customizations for prospective student social network 
- Developed new features and fixed defects for public-facing and campus-community web applications
- Maintained dozens of legacy applications, ranging widely in size and complexity
- Added test coverage and refactored legacy code, increasing confidence and decreasing cost of support
- Created RESTful web services to act as secure APIs between front-end applications and back-end systems
- Built internal libraries and frameworks to increase efficiency and consistency across projects and applications
- Worked directly with users and managers to develop requirements and prioritize feedback

### Software Developer / System Administrator, Performance Systems Development -- Ithaca, NY (2010 -- 2013)

- Developed desktop and web-based applications to model and track energy efficiency upgrades
- Built visual reports to aggregate building attributes and performance results across thousands of entities
- Profiled application and database performance, improving the speed of the application by as much as 120x
- Created a configurable workflow engine, allowing customers to customize how jobs are processed
- Created an extensible plugin framework, allowing customer-specific modules to be added dynamically
- Increased automated unit test code coverage of core assemblies to 100%
- Monitored and optimized virtual resource allocation, performed system administration duties
- Took on many Product Owner and Scrum Master responsibilities

### Programmer/Analyst / Investment Support, Loews Corporation -- New York, NY (2007 -- 2010)

- Developed desktop and web-based applications connected to back-end trading systems
- Deployed applications and system updates (both in-house and vendor supplied)
- Built customized, graphical reports on the company's investments and financial transactions
- Created user documentation for system setup and deployment, and specialized login / security practices
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

### Bachelor of Science, Computer Science

#### Ithaca College, 2003 -- 2007 (Cum Laude)
