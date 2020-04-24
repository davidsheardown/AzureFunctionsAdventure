## Azure Functions Adventure

A curious adventure using Azure functions, event grid and storage to build a not-so-simple but no-so-hard example microservice app.

 1. Create a REST API capable of the usual GET, PUT, POST, DELETE
 2. Use Azure storage tables as a cheap, raw tabular data store
 3. Use Azure storage queue as a cheap, basic queue mechanism
 4. Azure event grid for acustom pubsub (publish/subscribe) mechanism to help with inter-function (microservice) communication

All of the above, fortunately, shouldn't cost you a penny since the volumes/operations are pretty lean for dev/learning purposes.
You will need a Microsoft Azure account which you can setup a free/pay-as-you-go one - I will include links to the resources shortly.

If you don't want to be tied into to the Azure world, then we will also look at AWS Lambda for the functions/microservices and AWS SQS/S3 for queues and storage.  We will also look at using containers (docker) to host the Azure functions runtime which means you can have portability and host the above example microservice app where the hell you like :)

> Written with [StackEdit](https://stackedit.io/).
