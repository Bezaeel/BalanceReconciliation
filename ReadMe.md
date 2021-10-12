# Implementation of an EOD API
This API calculates EOD - End of day balances of the transactions over a given period <br/>
It is a rolling balance, the closing balance of yesterday is the opening balance of today; <br/>
EOD is the closing balance for the day

EOD is very important in accounting as it helps financial organizations check for consistency.

# How application works

The Reconciliation POST endpoint accepts a json file upload in a agreed template <br/> 
deserializes it then calculates the EOD of the transactions in the file.

# Testing the application

```
    cd BalanceReconciliation.Test
    dotnet test
```

# Running the application

`docker-compose up`

