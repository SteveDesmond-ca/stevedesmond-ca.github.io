---
title: Blog
---
## {{page.title}}
{% for item in site.posts %}
---
### [{{item.title}}]({{item.url}})
#### {{item.description}}
##### *{{item.date | date: '%B %-d, %Y'}}*
{% endfor %}