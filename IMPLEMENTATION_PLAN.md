# Implementation Plan - OstrohProblems

This plan outlines the steps to implement the requested changes for Categories, Problem Status/Map, and SignalR/Notifications.

## 1. Refactor Categories to Value Object

**Goal:** Replace the dynamic `Category` entity with a static `Category` Value Object (fixed list of ~10 categories) and remove all CRUD management for categories.

### API Changes
- [ ] **Create `Category` Value Object**
    - Create `api/src/Domain/Categories/Category.cs` as a `ValueObject` (similar to `IncidentStatus`).
    - Define static properties for ~10 predefined categories (e.g., "Roads", "Lighting", "Garbage", "Water", "Public Transport", "Parks", "Safety", "Noise", "Animals", "Other").
    - Implement `From(string)` and implicit/explicit operators.
- [ ] **Update `Problem` Entity**
    - Change `ICollection<Category> Categories` to use the new Value Object.
    - Update `ProblemConfiguration` to map categories (likely as a JSONB column or a primitive collection of strings in Postgres, effectively removing the `fk_problem_categories` join table).
- [ ] **Clean up Legacy Category Code**
    - Delete `CategoryRepository` (Interface & Implementation).
    - Delete `CategoriesController`.
    - Delete `CreateCategoryCommand`, `UpdateCategoryCommand`, `DeleteCategoryCommand`.
    - Delete `CategoryDto` and related mappers.
    - Remove `ICategoryQueries`.
- [ ] **Update Problem Commands**
    - Update `CreateProblemCommand` and `UpdateProblemCommand` to accept a list of string category names instead of GUIDs.
    - Validate that provided category names exist in the static `Category` list.

### Client Changes
- [ ] **Create Static Category Definitions**
    - Create `client/src/constants/categories.ts` with the list of available categories (matching API).
- [ ] **Remove Category Management**
    - Delete `features/categories` folder (API, hooks, components).
    - Delete `pages/CategoriesPage`.
    - Remove "Categories" link from Admin Dashboard/Sidebar.
- [ ] **Update Problem Creation/Edit Forms**
    - Replace `useCategories` query with import from `categories.ts`.
    - Update form logic to select categories from the static list.
- [ ] **Update Problem Display**
    - Update types to reflect that Category is just a string (or name) and not an object with ID.

## 2. Problem Status & Map Constraints

**Goal:** Add "Validated" status, update workflow, filter map data, and restrict map movement.

### API Changes
- [ ] **Update `ProblemStatus`**
    - Add `Validated` ("Провалідована") to `Domain/Problems/ProblemStatus.cs`.
    - Ensure `SupportedStatuses` includes it.
- [ ] **Update Map Data Endpoint (`/problems/get-all`)**
    - Modify the query in `ProblemRepository` or the relevant Handler.
    - **Filter Logic:** Return all problems **EXCEPT** those with status: `New`, `Completed`, `Rejected`.
        - *Result:* Only `Validated` and `InProgress` (and potentially others if added later) should be returned for the public map.
- [ ] **Access Control**
    - Ensure `GetAll` (or the specific map endpoint) allows Anonymous access (or specifically `RoleNames.User` + Guest).

### Client Changes
- [ ] **Map Bounds & Constraints**
    - Update `client/src/features/problems/components/problems-map.tsx` (or `Map` page).
    - Define `bounds` for Ostroh + 10km radius.
    - Configure `MapContainer` with `maxBounds`, `maxBoundsViscosity={1.0}`, and appropriate `minZoom`.
- [ ] **Map Page Access**
    - Ensure `routes` allow access to `/map` for non-logged-in users.
- [ ] **Status Workflow UI**
    - Update Coordinator UI to allow transitioning `New` -> `Validated`.
    - Update Coordinator/Admin lists to show the new status.

## 3. SignalR & Notifications

**Goal:** specific notification rules for Users vs. Coordinators/Admins and auto-refreshing lists.

### API Changes
- [ ] **Update Hubs**
    - **NotificationsHub**: Refine groups or user-specific messaging.
    - **CommentsHub**: Ensure real-time comment updates still work.
- [ ] **Implement Notification Rules**
    - **For Simple User:**
        - Send notification when:
            - Someone comments on their problem.
            - Someone rates their problem.
            - Coordinator changes status (New -> Validated, Validated -> InProgress, etc.).
    - **For Coordinator/Admin:**
        - **Suppress** standard notifications for these roles (as requested).
        - **Send "Refresh" Event:** specific event (e.g., `RefreshProblemList`) when a problem is created or status changes.
- [ ] **Trigger Points**
    - Inject `IHubContext` into `UpdateProblemStatusCommandHandler`, `CreateCommentCommandHandler`, `CreateRatingCommandHandler`.
    - Dispatch events based on the rules above.

### Client Changes
- [ ] **SignalR Listeners**
    - **User:** Listen for notifications -> Show Toast/Badge.
    - **Coordinator/Admin:** Listen for `RefreshProblemList` -> Trigger React Query invalidation (`queryClient.invalidateQueries(['problems'])`).
- [ ] **Admin Dashboard Update**
    - Update `/dashboard` to remove links to deleted Category pages.
    - Translate static text to Ukrainian.
    - Ensure the problem list auto-refreshes.
