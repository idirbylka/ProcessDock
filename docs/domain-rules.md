# Domain Rules

## Overview

The system enforces a set of rules that govern how entities relate to each other and how work is executed.

These rules define the current behavior of the system and are expected to evolve as additional capabilities are introduced.

---

## Entity Relationships

* A WorkItem must belong to a Project
* A WorkSession must belong to a WorkItem

These constraints ensure a consistent hierarchy and prevent orphaned data.

---

## WorkSession Lifecycle

A WorkSession represents a period of active work and follows a simple lifecycle:

* Running
* Stopped

State transitions are controlled by application logic.

---

## Execution Constraints (V1)

* Only one active (Running) WorkSession is allowed per WorkItem

This constraint simplifies execution tracking and avoids overlapping sessions in the initial version.

---

## Evolution of Execution Rules

As the system evolves to support multi-user scenarios, execution constraints may be refined.

In particular:

* Multiple concurrent WorkSessions may be allowed on the same WorkItem
* Constraints may shift toward user-scoped execution, such as:

```text
One active WorkSession per user per WorkItem
```

This enables collaborative execution while maintaining consistency.

---

## Time Tracking Rules

* Duration is derived from the difference between start and end timestamps
* All timestamps are stored in UTC
* Duration is persisted in seconds for efficient querying and aggregation

---

## Error Conditions

The system prevents invalid state transitions, including:

* Starting a WorkSession when one is already active under current constraints
* Stopping a WorkSession that is not in a running state

---

## Future Extensions

The domain rules are expected to expand to support:

* WorkItem lifecycle management
* aggregation of execution metrics
* approval workflows
* additional validation rules driven by business requirements
