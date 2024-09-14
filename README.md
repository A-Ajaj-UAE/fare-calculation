# fare-calculation
This solution allow a back office to calculate a driver payment based on multiple bands, where the number of band is dynamic and can be changed in the futire

# Analysing the document
the requirment discuss about multiple bands but did not discuss the possibility of having gaps
the requirment has a logical issue where the cost is become 2 for mile 2-6 and then 3 on next 10 mile then again 1 for extra miles
in this implementation I did assumtions and create a solution that can allow gaps as (optional validation) 
also I did ignore the logical error to be discussed for a possibility of bussness requirment


# data structure of bands
the ideal solution will be a rule base that add extra fare when the rule is applied

Date attriputes 
milefrom int - starting mile 
mileto int - ending mile 
rate decimal - the amount to pay extra when spent mile is between the mile range
IsBasedRate bool - IsBaseBand determin a band having a default rate for mile

To be adviced in the futre have a room for geolocation / class 


# solution archetechure 
The solution contains 2 projects with no much engineering to keep it simple
1- console based app, it hold the entity - validator and the utility fuction to do the calculation, 
it write the date on console as alternative of logs and has exception handeling 
2- unit test app, its connected to the main project and validate the important logics validator and calculation

# how to run the application
1- clone the project to your local
2- intall dependencies - .net 8
3- run the application