# AmazonReviewScrapper

##Solution explanation:

This solution should cover these two features:
* As an API Client, I want to be able to track reviews of a list of products at once and want to be alerted when the reviews I tracked are available.

-> The Client can track reviews of a list of product by add a list of ASINs to the product table. There a feature that scraps a review website looking for reviews in all pages. This feature should be  used ina scheduled job/azurefunction where the first call would be to retrieve all product tracked by the client (list of ASINs) using a GetTrackedProducts and then pass each item in this list to the ScrapWebsite feature which will retrieve review for every ASIN store them in a storage provider and send an email to notify the client if a review has been deleted/ a review has been added or a review content has been updated.

* As an API Client, I want to query the latest reviews of the products I have tracked.

-> The are two endpoint to retrieve reviews; one where the client can see all the reveiws and one where the client can see all the reviews for a certain ASIN

##Imporvements:

* Setup a static website ( mock amazon website) and add integration tests that run on that website/page.
* Rethink the storage provider for another more suitable to handle huge amounts of data; the sqllite used here can be easily swapped by another porovider ( only one project is implementing ef logic) and can be swapped with another project (CosmosDb ??) without breaking the solution.
* Use autofac for better Di management; create config project where you create autofac modules, inside those modules you can write the di config and then load those modules in the startup, that way we can have internal implementation and public interfaces only.
* Improve logging
* Improve exception handling
* Extract configs from code and store them in the config ( and when deployed in a keyVault)
* Use a push notification system instead of sending email to notify clients

##Note;

The modules "ReviewTracking" for the first feature and "Reviews" for the second feature , are independent and can be deployed seperatly.
