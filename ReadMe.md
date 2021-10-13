# Implementation of an EOD API
This API calculates EOD - End of day balances of the transactions over a given period <br/>
It is a rolling balance, the closing balance of yesterday is the opening balance of today; <br/>
EOD is the closing balance for the day

EOD is very important in accounting as it helps financial organizations check for consistency.

# How application works

The Reconciliation POST endpoint accepts a json file upload in a agreed template <br/> 
deserializes it then calculates the EOD of the transactions in the file.

# Running the application

`docker-compose up`

# Testing
After running the application <br/>

- Open browser <br/>
- Navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger)

# Sample
![1](Screenshot1.png)
![2](Screenshot2.png)


# Improvement
Considering this task to be CPU intensive, we can move the service project from a class library to a worker service <br/>
Employing a queue to make the EOD calculation process asynchronous. <br/>

Therefore, new architecture will be like 
![EOD](EOD.drawio.svg)

In the architecture above, we scale out the worker service based on request queueing.

Additional mechanisms will be added seeing this feature is async now.

- Endpoints to get EOD balances/pending status
- Webhook for EOD balances

These above mechanisms will retrieve the EOD from cache by a specified correlationID which was sent as acknowledgement from the queue.