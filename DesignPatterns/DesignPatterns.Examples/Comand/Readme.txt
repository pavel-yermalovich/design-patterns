Intent:

- Represent an action as an object
- Decouple clients that execute the command from the details and dependencies of the command logic
- Enables delayed execution:
    1. Can queue commands for later execution
    2. If command objects are also persistent, can delay across process restarts

Also known as:

- Action
- Transaction

Applicability:

- Logging
- Validation
- Undo