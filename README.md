# AES-ModVal-BancoABC

Taller de implementación para la clase de Modelado y Validación de Arquitectura de la especialización de Arquitectura Empresarial de Software de la Pontificia Universidad Javeriana, sede Bogotá, Colombia.

## Getting Started
El presente proyecto contiene los recursos necesarios para solucionar el problema del Banco ABC, el cuál tiene convenios con diferentes proveedores de servicios públicos, y quiere que estos puedan ser "conectados" de manera dinámica, sin mayores afectaciones a la arquitectura.

## Arquitectura propuesta
Dada las necesidades del Banco ABC, se requiere que la solución de arquitectura propuesta fomente el gobierno y la adaptabilidad del sistema de pagos de servicios públicos. Para esto se ha dispuesto de un conjunto de componentes, que siguiendo la filosofía de la Orientación a Servicios (SOA), van a ayudar a dar una solución que permita evolucionar al negocio teniendo en cuenta estas necesidades.

### Contenedores
Cada componente fue desplegado en contenedores (Docker) que permiten ser autocontenidos e independientes. Esto permite tener mayor control en aspectos como redundancia sobre las instancias y evolución independiente de las mismas.

### Service Registry
... proximamente...

### Contract First
El método de definición de contratos para servicios internos utilizado es Contract First, debido a que los contratos fueron definidos pensando en entidades de negocio por encima de cualquier otro aspecto técnico.

### Intermediate Routing
Fue necesaria la implementación del patrón de enrutamiento para poder determinar de manera dinámica, la ruta que debe seguir una petición dada la información (primeros 4 dígitos) contenida en el número de referencia de la factura.

### Data Model Transformation
Se implementó el patrón de transformación de modelo de datos para que el sistema pueda ser dinámico ante cualquier convenio y sus datos, sin importar que usen  esquemas diferentes al del Banco ABC.

### Dispatcher
El despachador fue usado como orquestador de la composición, y es el encargado de exponer el servicio de Pago con sus capacidades de Consultar, Pagar y Compensar. La lógica de este servicio invoca al enrutador, al transformador, y envía las peticiones al convenio de destino.

### API Gateway
Sólo los servicios de Consultar, Pagar y Compensar serán expuestos al mundo exterior a través de un API Gateway en el que se registrarán para que puedan ser consumidos por los diferentes canales que ofrece el Banco ABC.
