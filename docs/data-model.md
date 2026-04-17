# Data Model

## Overview

ProcessDock is structured around a hierarchical model that reflects how work is organized and executed within a project.

The core entities are:

* Project
* WorkItem
* WorkSession

---

## Project

Represents a top-level container for organizing work.

Projects define the scope within which work items are created and managed.

**Relationships:**

* One Project → Many WorkItems

---

## WorkItem

Represents a unit of work within a project.

WorkItems define what needs to be executed and serve as the anchor point for execution tracking.

**Relationships:**

* Belongs to one Project
* One WorkItem → Many WorkSessions

---

## WorkSession

Represents a tracked execution of work on a WorkItem.

WorkSessions capture when work occurs and how long it takes.

**Key Attributes:**

* StartedAtUtc
* EndedAtUtc
* DurationSeconds
* Status
* Notes

**Relationships:**

* Belongs to one WorkItem

---

## Structure

```text
Project
  └── WorkItem
        └── WorkSession
```

---

## Modeling Considerations

* WorkSessions are treated as discrete execution records
* Duration is derived from timestamps and stored for efficient querying
* The model is designed to support future extensions such as:

  * concurrent sessions
  * aggregation of operational metrics
  * cost and performance analysis

---

## Evolution

The current structure provides a stable foundation for execution tracking.

It is expected to evolve to support more complex scenarios, including multi-user collaboration and richer operational data.
