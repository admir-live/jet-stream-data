
# Getting Started with Our Flight Information Service

Welcome to our Flight Information Service, a simple yet powerful tool designed to provide you with up-to-date flight details. Whether you're looking for specific flights or browsing through our database, our service is structured to meet your needs efficiently. This guide will walk you through setting up a MySQL database using Docker and interacting with our service through example requests.

## Prerequisites

Before you begin, ensure you have Docker installed on your machine. Docker allows you to run applications and databases inside containers, providing an isolated environment that's consistent across any platform.

## Setting Up MySQL Database with Docker

Our service relies on a MySQL database to store flight information. We've made setting up this database straightforward by using Docker. Below is the Docker Compose file necessary to start the MySQL database:

```yaml
version: '3.8'

services:
  mysql:
    image: mysql:5.7
    environment:
      MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD} # MySQL root password has been setup in '.env'
      MYSQL_DATABASE: FlightDb
    volumes:
      - mysql-data:/var/lib/mysql
    ports:
      - "33060:3306"
    networks:
      - backend
    healthcheck:
      test: ["CMD", "mysqladmin" ,"ping", "-h", "localhost"]
      timeout: 20s
      retries: 10

networks:
  backend:
    driver: bridge

volumes:
  mysql-data:
    driver: local
```

1. **Save this configuration as `docker-compose.yml`** in your desired directory.
2. **Run the database container** using the command: `docker-compose up -d`. This command starts the MySQL database in detached mode, running in the background.
3. **Ensure the database is running** by checking the logs: `docker-compose logs -f mysql`.

## Interacting with the Flight Information Service

After setting up the MySQL database, you can interact with our Flight Information Service through HTTP requests. Below are examples of how to make requests and understand the responses:

### Example Requests

1. **Fetch flights by keyword, page size, and order:**

```
http://localhost:5137/api/v1/flights?Keyword=AF1380&PageSize=2&Page=1&OrderBy=Flight.Number&Direction=desc
```

2. **Narrow down search results using different keywords and pagination:**

```
http://localhost:5137/api/v1/flights?Keyword=380&PageSize=1&Page=2&OrderBy=Flight.Number&Direction=desc
```

### Example Response

```json
{
  "PageIndex": 1,
  "PageSize": 2,
  "TotalCount": 1,
  "TotalPages": 1,
  "Items": [
    {
      "Flight": {
        "Number": "AF1380",
        "Status": "Cancelled"
      },
      "DepartureAirport": {
        "Name": "Paris",
        "IataCode": "Europe/Paris",
        "IcaoCode": "CDG",
        "Timezone": "LFPG"
      },
      "ArrivalAirport": {
        "Name": "Mexico City",
        "IataCode": "America/Mexico_City",
        "IcaoCode": "MEX",
        "Timezone": "MMMX"
      },
      ...
    }
  ]
}
```

This response gives you detailed information about the flight, including flight number, status, departure, and arrival details.

## Conclusion

### Conclusion and Technical Approach
In the development of our Flight Information Service, we've adopted a comprehensive and advanced set of methodologies and technologies to ensure a robust, scalable, and maintainable architecture. Our commitment to best practices and cutting-edge solutions is reflected in every layer of our application. Below, we detail the core principles and technologies that underpin our service:

### Domain-Driven Design (DDD)
We've employed Domain-Driven Design principles to align our development closely with the complex domain of flight information management. This approach has helped us to create a model that accurately reflects real-world flight operations, facilitating communication between technical and domain experts and ensuring our software architecture directly solves domain-specific problems.

### Advanced C# and .NET
Leveraging the latest features of C# and the .NET ecosystem has allowed us to develop a high-performance, secure, and scalable service. Advanced C# features have enabled us to write cleaner, more expressive, and efficient code, while the .NET platform provides a robust foundation for building cross-platform applications with strong community support.

### Command Query Responsibility Segregation (CQRS)
By adopting the CQRS pattern, we've separated read and write operations, optimizing performance, scalability, and security. This approach not only enhances the system's responsiveness and user experience but also simplifies the maintenance and evolution of the service by decoupling the data model used for updates from the model used for reads.

### Clean Architecture
Our adherence to Clean Architecture principles ensures that our application's domain logic is independent of UI, database, and external integrations. This separation of concerns makes our system more testable, understandable, and adaptable to changes in external technologies or business requirements.

### Docker
Docker has been instrumental in creating a consistent development, testing, and production environment. By containerizing our service and its dependencies, we've streamlined deployment processes and eliminated the "it works on my machine" problem, ensuring our service runs reliably and efficiently on any platform.

### Principles of SOLID and Proof of Concept (POC)
Throughout the development process, we've designed our Proof of Concept (POC) to adhere strictly to the SOLID principles, ensuring our software is easy to maintain, extend, and scale. This adherence to SOLID principles has guided our architectural decisions, promoting a high degree of flexibility and reducing the risk of future code smells or refactoring needs.

By combining Domain-Driven Design, advanced features of C# and .NET, CQRS, Clean Architecture, and Docker, and ensuring our designs meet the SOLID principles through a carefully crafted POC, we've created a service that stands at the forefront of modern software development practices. Our approach not only addresses current requirements efficiently but also lays a strong foundation for future expansion and adaptation to new challenges.
