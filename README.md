# MajesticRealtors
<img src="https://github.com/PreethamGoud/MajesticRealtors.git" width="350" alt="accessibility text">

# Introduction

This website aims at providing users a one stop portal for finding their dream home and land to setup their business in the City of Chicago.  <br/>  

•	Enter Area Name / Area Number in City of Chicago.  <br/>  
•	View the apartments/houses list.  <br/>  
•	Also find the empty spaces nearby to setup your business (At lowest cost provided by the City Government itself).  <br/>  

# DataFeeds

<a href="https://data.cityofchicago.org/resource/aksk-kvfp.json">City Owned Land Inventory </a><br/>
<a href="https://data.cityofchicago.org/resource/s6ha-ppgi.json">Affordable Rental Housing </a><br/>

# Requirements

Requirement_1: Find your Housing
Scenario: As a business owner when I move from different city to Chicago or I being a resident needs to set up a business, I want to be able to search places in Chicago for affordable housing for my workforce or myself.
Assumptions: Click on Housing and select the Community Area you are interested in living in.
Examples:
Given a selected area as Englewood.
When I search for Housing/Apartments in that area
Then I should receive list of all available Housing and its details in that area.

Victoria Jennings Residences	624 W. 61st St.	Englewood	68
57	773-994-3690	HSR Property Services, LLC

Hope Manor Village	5900-6100 S. Green/Peoria/Sangamon	Englewood	68
36	312-564-2393	Volunteers of America Illinois

Requirement_2: Find the land to setup your business
Scenario: As a business owner when I move from a different city to Chicago or I being a resident needs to set up a business, I want to be able to search places in Chicago for setting up my business at affordable prices. So, I will check the city owned land inventory data. I may also use community numbers to find the land for my business.
Assumptions: Click on Lands and under Community Number, enter the relevant area number you are interested in living in.
The search term is Community Area Number pertaining to the city of Chicago only. Community Area Number is a 2-digit number.
Examples:
Given a Community Area Number is 23
When I search for land details in that community
Then I should receive list of all available land details in that community

Owned by City	DPD - Planning	HUMBOLDT PARK	3625 W CHICAGO AVE	60651	9683	41.89523451453931	-87.71717748680612

Sold	None	HUMBOLDT PARK	3228 W DIVISION ST	60651	3099	41.90309891359026	-87.70810659881111

# Scrum Roles

•	Frontend Developer: Surya Sanjay Bandari  <br/>
•	Integration Developer: Preetham Chelimela  <br/>
•	DevOps/Scrum Master: Lloyd Dsouza  <br/>
•	Backend Developer/API: Shyam Varma  <br/>

# Weekly Meeting

Wednesday at 05:00 PM on Teams



