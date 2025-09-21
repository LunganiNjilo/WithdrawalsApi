# Withdrawals Api

This is the refactored version of the bank account withdrawal code improvement exercise:

### Approach: Clean Architecture & SOLID Principles

#### Structure:

- Domain Layer: Core entities (Account, WithdrawalEvent) and business rules.
- Application Layer: Services like WithdrawalService, orchestrating operations.
- Infrastructure Layer: In-memory stubs for repositories (IAccountRepository) and event publishing (IEventPublisher) to simulate DB and messaging.
- Presentation Layer: Controllers exposing endpoints (e.g., /withdrawal).

#### Benefits Achieved:

- Separation of concerns, adherence to SOLID principles.
- Testable, maintainable, and flexible design.
- Stubs allow end-to-end workflow testing without external infrastructure.

### Identified Bug: Unreachable Code

- Original code had a return statement before event publishing, making the withdrawal event unreachable.
- Result: Successful withdrawals did not trigger events, breaking observability and auditability.

### How We Discovered the Bug

- Primary: Integration tests using the in-memory stubs.
Withdrawal balance updated, but the event was never published.
- Secondary: Static analysis / IDE warnings could highlight unreachable code.
- Note: Code coverage could also help, but VS Community edition lacks this feature.

### Testing with Postman

1. Open **Postman**.  
2. Create a new **POST** request.  
3. Set the URL:  

http://localhost:5218/api/bankaccount/withdraw

4. In the **Body**, choose **raw → JSON** and paste:  

```json
{
  "accountId": "11111111-1111-1111-1111-111111111111",
  "amount": 200
}

```

5. Send the request.  

#### ✅ Expected Responses
- **200 OK** → `"Withdrawal successful"`  
- **400 Bad Request** → `"Withdrawal failed or insufficient funds"`  



### Test Cases

| Test Case              | AccountId                              | Amount | Expected Result | Observed Result | Notes                                  |
|------------------------|-----------------------------------------|--------|-----------------|-----------------|----------------------------------------|
| Successful withdrawal  | 11111111-1111-1111-1111-111111111111    | 200    | Success         | Success         | Balance updated, event published        |
| Full withdrawal        | 11111111-1111-1111-1111-111111111111    | 1000   | Success         | Success         | Balance 0, event logged                 |
| Over-limit withdrawal  | 11111111-1111-1111-1111-111111111111    | 1200   | Failed          | Failed          | Insufficient funds          |

Successful withdrawal
<img width="973" height="225" alt="image" src="https://github.com/user-attachments/assets/55e9aa14-c426-49e6-9a16-4cd655f13c55" />

Full withdrawal
<img width="976" height="227" alt="image" src="https://github.com/user-attachments/assets/a483e256-03e4-4154-a488-d4f9def8ebae" />

Over-limit withdrawal
<img width="972" height="213" alt="image" src="https://github.com/user-attachments/assets/6edaa9b5-8916-47f9-9ddc-c7c251529eee" />

## How the Solution Covers Key Qualities

| Quality              | How It’s Addressed                                                                 |
|----------------------|-------------------------------------------------------------------------------------|
| **Efficiency**       | In-memory stubs provide fast read/write; async publishing prevents blocking.        |
| **Throughput**       | Async operations allow concurrent withdrawals.                                     |
| **Maintainability**  | Clear separation of concerns; easy to swap implementations.                        |
| **Flexibility**      | Interfaces allow multiple implementations for DB/messaging.                        |
| **Consistency**      | Domain layer enforces rules consistently.                                          |
| **Fault Tolerance**  | Abstraction allows retries and safe simulation of failures.                         |
| **Testability**      | In-memory stubs and DI support unit/integration tests.                             |
| **Dependency Management** | Services depend on interfaces, not concrete implementations.                  |
| **Observability**    | Events capture withdrawal actions; logs provide system insight.                     |
| **Auditability**     | Event data (AccountId, Amount, Status) enables auditing.                            |
| **Portability**      | Self-contained; runs on Windows, Linux, Docker.                                    |
| **Correctness**      | Business rules enforced; tests verify expected outcomes.                           |
| **Cost Efficiency**  | In-memory stubs reduce need for DB/messaging infrastructure.                        |
| **Data Governance**  | Controlled updates via repository and service abstractions.                         |
| **Interoperability** | Event publishing interface supports multiple messaging systems.                     |

### Side Notes & Observations

- Security: Inline SQL queries are vulnerable to SQL injection (out of scope, but noted).
- Integration Tests: Out of scope for the implementation, but essential for detecting the unreachable code.
- Event Publishing: In-memory publisher verifies end-to-end flow without Kafka/SNS.
- Maintainability & Extensibility: Clear separation allows future integration with real infrastructure.
  
