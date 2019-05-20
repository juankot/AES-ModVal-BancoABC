
# API Gateway

### Se crea la red kong-net
docker network create kong-net

### Se instancia la BD de cassandra para Kong
docker run -d --name kong-database --network=kong-net -p 9042:9042 cassandra:3

### Se configura la BD de cassandra para Kong
docker run --rm --network=kong-net -e "KONG_DATABASE=cassandra" -e "KONG_PG_HOST=kong-database" -e "KONG_CASSANDRA_CONTACT_POINTS=kong-database" kong:latest kong migrations bootstrap

### Se configura Kong
docker run -d --name kong --network=kong-net -e "KONG_DATABASE=cassandra" -e "KONG_PG_HOST=kong-database" -e "KONG_CASSANDRA_CONTACT_POINTS=kong-database" -e "KONG_PROXY_ACCESS_LOG=/dev/stdout" -e "KONG_ADMIN_ACCESS_LOG=/dev/stdout" -e "KONG_PROXY_ERROR_LOG=/dev/stderr" -e "KONG_ADMIN_ERROR_LOG=/dev/stderr" -e "KONG_ADMIN_LISTEN=0.0.0.0:8001, 0.0.0.0:8444 ssl" -p 8000:8000 -p 8443:8443 -p 8001:8001 -p 8444:8444 kong:latest

### Se instala e instancia Konga para Kong
$ docker pull pantsel/konga
$ docker run -p 1337:1337 --network kong-net --name konga -e  "NODE_ENV=development" -e "TOKEN_SECRET=sgdfguidhkgjdfhgkjdfh" pantsel/konga

### Registro del Servicio
curl -i -X POST http://172.19.0.3:8001/services/ -d 'name=api-dispatcher' -d 'url=http://10.138.21.90/Dispatcher/v1/dispatcher'

### Registro de enrutamiento
curl -i -X POST http://172.19.0.3:8001/services/api-dispatcher/routes/ -d 'paths[]=/api'

### test
curl -i -X GET --url http://172.19.0.3:8000/api
