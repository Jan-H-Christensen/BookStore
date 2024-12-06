# Book store Assignment :memo:

In our application, we deliberately opted for a combination of a relational database (MSSQL) and a NoSQL database (Redis), as both technologies have different strengths that complement each other perfectly.

## **Relational database (MSSQL)** :card_file_box:

MSSQL serves as our main database as it is ideal for structured data that is in fixed relationships to each other. The advantages are:

### **Complex Queries**

MSSQL enables efficient JOINs and complex SQL queries, which are essential for generating detailed reports and performing in-depth analyses. This is particularly critical in a complex order and inventory management system, where data is highly interconnected and needs to be retrieved, updated, and analyzed in a structured manner.Such a database is indispensable, especially for business-critical processes such as the management of orders and stock levels. It not only ensures high data quality, but also the ability to respond quickly and effectively to business challenges

Scalability and reliability: Proven technology for long-term data management.

## **NoSQL database (Redis)** :zap:

Redis is used to fulfill specific requirements where speed and flexibility are paramount. Typical application scenarios are:

### **Caching**

Redis enables extremely fast data queries, which significantly boosts the performance of our application. By using Redis, we can offload frequent read operations from our primary database, reducing its load and improving the overall system responsiveness.

In our application, Redis is specifically used for caching our Books and Authors data. This decision was made because these datasets can grow significantly, both in volume and in the frequency of access. Books and Authors are often queried together in scenarios such as search functionality, recommendations, or browsing operations. Relying solely on the primary database for such operations could lead to slower response times and unnecessary strain on the database, especially during peak usage.
Redis is also great in storing session data or other information for Sessions and temporary data.
NoSQL systems such as Redis are optimized for high read and write loads.

## **Why we choose to have all in a relational database** :bulb:

Relational databases are optimized for queries that exploit relationships between data.
**Examples:**

“Which books were written by a particular author?”
“How many books have multiple authors?”
“Which authors have published in a certain period?”

Such queries require efficient JOINs, which are executed quickly and precisely in relational databases. In NoSQL systems, such as a document-oriented approach (e.g. MongoDB), data would either have to be duplicated (leading to redundancy) or additional processing steps would have to be implemented to calculate these relationships.
NoSQL databases would of course give us some flexibility but it could lead to inconsistencies if not done with some responsibility.

An other reason why we didn't used a NoSQL database for Books and Authors is, it often would be stored as separate documents or nested structures. This leads to  
data redundancy for example author information could be stored multiple times in different books. Changes (e.g. correction of a name) would have to be made in many places, which makes data maintenance more difficult.

## **Conclusion**

For these reasons, we came to the conclusion that it is more efficient to store books and authors in a relational database and to index them well. This allows frequent queries to be answered quickly. In this case, NoSQL should serve as a supplement, not a replacement.

By combining both technologies, we utilize the advantages of both approaches. On the one side the robustness and structure of a relational database and on the other side the speed and flexibility of a NoSQL solution. This enables us to develop a high-performance yet reliable application that can respond optimally to different requirements.

## Setup :rocket:

To run this application, simply execute the following command:
´´´
docker-compose up -d
´´´
Once the application is built in Docker, a SQL script will automatically run after 60 seconds to set up the database with some initial data.
After the setup is complete, you can test the application by entering...
´´´
localhost:8080/swagger
´´´
You can enter it directly into your browser or test it in Postman.
´´´
http://localhost:8080/GetBooks
´´´
´´´
http://localhost:8080/GetBooksId
´´´
´´´
http://localhost:8080/GetAuthor
´´´
´´´
http://localhost:8080/GetInventory
´´´
´´´
http://localhost:8080/CreateOrder
´´´
´´´
http://localhost:8080/UpdateInventory
´´´
