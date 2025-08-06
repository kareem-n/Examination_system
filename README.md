# 🧪 Examination Management System
Tech Stack: ASP.NET Core, Onion Architecture, Entity Framework Core, RabbitMQ, SQL Server

Designed and developed a scalable Examination Management System to streamline exam creation, submission, evaluation, and result notification workflows, applying clean architectural principles and message-driven communication.

🧩 Key Features:
Layered and Maintainable Architecture: Built using Onion Architecture to enforce clear separation of concerns between core domain logic, application services, and infrastructure.

Asynchronous Communication:

Implemented RabbitMQ to decouple exam submission from evaluation.

Exams are submitted via the main application and pushed to a message queue for asynchronous processing and evaluation.

Evaluation results are sent back through RabbitMQ and trigger notifications to users.

Exam Lifecycle Management:

Admins can create subjects, questions, and exams with varying difficulty levels.

Students can enroll, take timed exams, and submit answers.

Evaluation is handled automatically or manually depending on question types.

Notifications & Status Updates: Integrated real-time or queued notifications to inform users when their exam has been evaluated.

Database Design: Used Entity Framework Core with SQL Server to manage complex relationships (users, exams, questions, answers, results).

Scalability: The system is designed to support high concurrency and future extension (e.g., reporting, analytics, multi-tenant setup).
