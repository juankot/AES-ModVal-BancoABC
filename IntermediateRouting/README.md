# IntermediateRouting
routing service with docker config ready to deploy.

this service is a .NET Core 2.1 web api wich main function is provide a third party consume configuration.

the service obey the "Contract First" SOA principle

the following is the main resource of the API :

http://localhost:9090/api/RouteOperationNumeroReferencia?operation={operation}&numeroReferencia={ref}

where the 'operation' parameter is the operation to be consulted and 'numeroReferencia' is the invoice id to be paid.

the following is an example response:
```
{
    "routing": {
        "provider": "Gas",
        "identifier": "1234",
        "type": "SOAP",
        "endpoint": "http://ec2-54-236-241-13.compute-1.amazonaws.com:8080/gas-service/PagosService",
        "action": "Consultar",
        "xsltRequest": "",
        "xsltResponse": ""
    },
    "resultado": {
        "descripcion": "Ok"
    }
}
```
