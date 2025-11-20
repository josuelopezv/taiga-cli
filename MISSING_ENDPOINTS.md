# Missing Taiga API Endpoints

This document tracks Taiga API endpoints that are not yet implemented in the CLI.

## Authentication & Users

- [x] POST `/auth` - Normal login
- [ ] POST `/auth` - GitHub login
- [ ] POST `/auth` - GitLab login
- [ ] POST `/auth` - Bitbucket login
- [ ] POST `/auth` - Google login
- [ ] POST `/auth` - LDAP login
- [ ] POST `/auth/register` - User registration
- [ ] POST `/auth/password-recovery` - Password recovery
- [ ] POST `/auth/change-password` - Change password
- [ ] POST `/auth/change-password-from-recovery` - Change password from recovery token
- [x] GET `/users/me` - Get current user
- [x] GET `/users/{id}` - Get user by ID
- [x] GET `/users` - List users
- [x] PATCH `/users/{id}` - Update user
- [x] GET `/users/{id}/stats` - Get user statistics

## Projects

- [x] GET `/projects` - List projects
- [x] GET `/projects/{id}` - Get project by ID
- [x] POST `/projects` - Create project
- [x] PATCH `/projects/{id}` - Update project
- [x] DELETE `/projects/{id}` - Delete project
- [x] GET `/projects/{id}/stats` - Get project statistics
- [x] GET `/projects/{id}/modules` - Get project modules
- [x] PATCH `/projects/{id}/modules` - Update project modules
- [x] GET `/projects/{id}/memberships` - Get project memberships
- [x] POST `/projects/{id}/memberships` - Create project membership
- [x] GET `/projects/{id}/memberships/{membershipId}` - Get membership
- [x] PATCH `/projects/{id}/memberships/{membershipId}` - Update membership
- [x] DELETE `/projects/{id}/memberships/{membershipId}` - Delete membership
- [x] GET `/projects/{id}/roles` - Get project roles
- [ ] GET `/projects/{id}/fans` - Get project fans
- [ ] POST `/projects/{id}/fans` - Add project fan
- [ ] DELETE `/projects/{id}/fans/{userId}` - Remove project fan
- [ ] GET `/projects/{id}/starred` - Check if project is starred
- [ ] POST `/projects/{id}/starred` - Star project
- [ ] DELETE `/projects/{id}/starred` - Unstar project
- [ ] GET `/projects/{id}/watchers` - Get project watchers
- [ ] POST `/projects/{id}/watchers` - Add project watcher
- [ ] DELETE `/projects/{id}/watchers/{userId}` - Remove project watcher
- [ ] GET `/projects/{id}/duplicate` - Duplicate project
- [ ] POST `/projects/{id}/duplicate` - Create duplicate project
- [ ] GET `/projects/{id}/export` - Export project
- [ ] POST `/projects/{id}/import` - Import project

## User Stories

- [x] GET `/userstories` - List user stories
- [x] GET `/userstories/{id}` - Get user story by ID
- [x] POST `/userstories` - Create user story
- [x] PATCH `/userstories/{id}` - Update user story
- [x] DELETE `/userstories/{id}` - Delete user story
- [x] GET `/userstories/{id}/attachments` - Get user story attachments
- [x] POST `/userstories/{id}/attachments` - Upload attachment
- [x] GET `/userstories/{id}/attachments/{attachmentId}` - Get attachment
- [x] DELETE `/userstories/{id}/attachments/{attachmentId}` - Delete attachment
- [x] GET `/userstories/{id}/history` - Get user story history
- [ ] GET `/userstories/{id}/upvote` - Upvote user story
- [ ] POST `/userstories/{id}/upvote` - Add upvote
- [ ] DELETE `/userstories/{id}/upvote` - Remove upvote
- [ ] GET `/userstories/{id}/downvote` - Downvote user story
- [ ] POST `/userstories/{id}/downvote` - Add downvote
- [ ] DELETE `/userstories/{id}/downvote` - Remove downvote
- [ ] GET `/userstories/{id}/watchers` - Get user story watchers
- [ ] POST `/userstories/{id}/watchers` - Add watcher
- [ ] DELETE `/userstories/{id}/watchers/{userId}` - Remove watcher
- [ ] POST `/userstories/{id}/promote` - Promote user story to epic
- [ ] POST `/userstories/{id}/convert` - Convert user story to issue

## Tasks

- [x] GET `/tasks` - List tasks
- [x] GET `/tasks/{id}` - Get task by ID
- [x] POST `/tasks` - Create task
- [x] PATCH `/tasks/{id}` - Update task
- [x] DELETE `/tasks/{id}` - Delete task
- [x] GET `/tasks/{id}/attachments` - Get task attachments
- [x] POST `/tasks/{id}/attachments` - Upload attachment
- [x] GET `/tasks/{id}/attachments/{attachmentId}` - Get attachment
- [x] DELETE `/tasks/{id}/attachments/{attachmentId}` - Delete attachment
- [x] GET `/tasks/{id}/history` - Get task history
- [ ] GET `/tasks/{id}/upvote` - Upvote task
- [ ] POST `/tasks/{id}/upvote` - Add upvote
- [ ] DELETE `/tasks/{id}/upvote` - Remove upvote
- [ ] GET `/tasks/{id}/downvote` - Downvote task
- [ ] POST `/tasks/{id}/downvote` - Add downvote
- [ ] DELETE `/tasks/{id}/downvote` - Remove downvote
- [ ] GET `/tasks/{id}/watchers` - Get task watchers
- [ ] POST `/tasks/{id}/watchers` - Add watcher
- [ ] DELETE `/tasks/{id}/watchers/{userId}` - Remove watcher

## Issues

- [x] GET `/issues` - List issues
- [x] GET `/issues/{id}` - Get issue by ID
- [x] POST `/issues` - Create issue
- [x] PATCH `/issues/{id}` - Update issue
- [x] DELETE `/issues/{id}` - Delete issue
- [x] GET `/issues/{id}/attachments` - Get issue attachments
- [x] POST `/issues/{id}/attachments` - Upload attachment
- [x] GET `/issues/{id}/attachments/{attachmentId}` - Get attachment
- [x] DELETE `/issues/{id}/attachments/{attachmentId}` - Delete attachment
- [x] GET `/issues/{id}/history` - Get issue history
- [ ] GET `/issues/{id}/upvote` - Upvote issue
- [ ] POST `/issues/{id}/upvote` - Add upvote
- [ ] DELETE `/issues/{id}/upvote` - Remove upvote
- [ ] GET `/issues/{id}/downvote` - Downvote issue
- [ ] POST `/issues/{id}/downvote` - Add downvote
- [ ] DELETE `/issues/{id}/downvote` - Remove downvote
- [ ] GET `/issues/{id}/watchers` - Get issue watchers
- [ ] POST `/issues/{id}/watchers` - Add watcher
- [ ] DELETE `/issues/{id}/watchers/{userId}` - Remove watcher

## Epics

- [x] GET `/epics` - List epics
- [x] GET `/epics/{id}` - Get epic by ID
- [x] POST `/epics` - Create epic
- [x] PATCH `/epics/{id}` - Update epic
- [x] DELETE `/epics/{id}` - Delete epic
- [x] GET `/epics/{id}/attachments` - Get epic attachments
- [x] POST `/epics/{id}/attachments` - Upload attachment
- [x] GET `/epics/{id}/attachments/{attachmentId}` - Get attachment
- [x] DELETE `/epics/{id}/attachments/{attachmentId}` - Delete attachment
- [x] GET `/epics/{id}/history` - Get epic history
- [x] GET `/epics/{id}/related-userstories` - Get related user stories
- [x] POST `/epics/{id}/related-userstories` - Add related user story
- [x] DELETE `/epics/{id}/related-userstories/{userStoryId}` - Remove related user story

## Milestones (Sprints)

- [x] GET `/milestones` - List milestones
- [x] GET `/milestones/{id}` - Get milestone by ID
- [x] POST `/milestones` - Create milestone
- [x] PATCH `/milestones/{id}` - Update milestone
- [x] DELETE `/milestones/{id}` - Delete milestone
- [x] GET `/milestones/{id}/stats` - Get milestone statistics
- [x] GET `/milestones/{id}/burndown` - Get burndown chart data
- [x] GET `/milestones/{id}/userstories` - Get milestone user stories
- [x] POST `/milestones/{id}/userstories` - Add user story to milestone
- [x] DELETE `/milestones/{id}/userstories/{userStoryId}` - Remove user story from milestone

## Wiki

- [x] GET `/wiki` - List wiki pages
- [x] GET `/wiki/{id}` - Get wiki page by ID
- [x] POST `/wiki` - Create wiki page
- [x] PATCH `/wiki/{id}` - Update wiki page
- [x] DELETE `/wiki/{id}` - Delete wiki page
- [x] GET `/wiki/{id}/attachments` - Get wiki page attachments
- [x] POST `/wiki/{id}/attachments` - Upload attachment
- [x] GET `/wiki/{id}/attachments/{attachmentId}` - Get attachment
- [x] DELETE `/wiki/{id}/attachments/{attachmentId}` - Delete attachment
- [x] GET `/wiki/{id}/history` - Get wiki page history

## Webhooks

- [x] GET `/webhooks` - List webhooks
- [x] GET `/webhooks/{id}` - Get webhook by ID
- [x] POST `/webhooks` - Create webhook
- [x] PATCH `/webhooks/{id}` - Update webhook
- [x] DELETE `/webhooks/{id}` - Delete webhook
- [x] POST `/webhooks/{id}/test` - Test webhook
- [x] GET `/webhooks/{id}/logs` - Get webhook logs

## Notifications

- [x] GET `/notifications` - List notifications
- [x] GET `/notifications/{id}` - Get notification by ID
- [x] PATCH `/notifications/{id}` - Mark notification as read
- [x] PATCH `/notifications/read` - Mark all notifications as read
- [x] GET `/notifications/unread` - Get unread notifications count

## Search

- [x] GET `/search` - Search across projects
- [x] GET `/search/{projectId}` - Search within project

## Activity

- [x] GET `/timeline/{projectId}` - Get project timeline
- [x] GET `/timeline/{projectId}/profile` - Get profile timeline
- [x] GET `/timeline/{projectId}/user/{userId}` - Get user timeline

## Custom Attributes

- [ ] GET `/userstory-custom-attributes` - List user story custom attributes
- [ ] POST `/userstory-custom-attributes` - Create user story custom attribute
- [ ] GET `/userstory-custom-attributes/{id}` - Get custom attribute
- [ ] PATCH `/userstory-custom-attributes/{id}` - Update custom attribute
- [ ] DELETE `/userstory-custom-attributes/{id}` - Delete custom attribute
- [ ] GET `/task-custom-attributes` - List task custom attributes
- [ ] POST `/task-custom-attributes` - Create task custom attribute
- [ ] GET `/task-custom-attributes/{id}` - Get custom attribute
- [ ] PATCH `/task-custom-attributes/{id}` - Update custom attribute
- [ ] DELETE `/task-custom-attributes/{id}` - Delete custom attribute
- [ ] GET `/issue-custom-attributes` - List issue custom attributes
- [ ] POST `/issue-custom-attributes` - Create issue custom attribute
- [ ] GET `/issue-custom-attributes/{id}` - Get custom attribute
- [ ] PATCH `/issue-custom-attributes/{id}` - Update custom attribute
- [ ] DELETE `/issue-custom-attributes/{id}` - Delete custom attribute
- [ ] GET `/epic-custom-attributes` - List epic custom attributes
- [ ] POST `/epic-custom-attributes` - Create epic custom attribute
- [ ] GET `/epic-custom-attributes/{id}` - Get custom attribute
- [ ] PATCH `/epic-custom-attributes/{id}` - Update custom attribute
- [ ] DELETE `/epic-custom-attributes/{id}` - Delete custom attribute

## Severities & Priorities

- [x] GET `/severities` - List severities
- [x] GET `/priorities` - List priorities
- [x] GET `/issue-statuses` - List issue statuses
- [x] GET `/issue-types` - List issue types
- [x] GET `/task-statuses` - List task statuses
- [x] GET `/userstory-statuses` - List user story statuses

## Comments

- [x] GET `/userstories/{id}/comments` - Get user story comments
- [x] POST `/userstories/{id}/comments` - Create comment
- [x] GET `/tasks/{id}/comments` - Get task comments
- [x] POST `/tasks/{id}/comments` - Create comment
- [x] GET `/issues/{id}/comments` - Get issue comments
- [x] POST `/issues/{id}/comments` - Create comment
- [x] GET `/epics/{id}/comments` - Get epic comments
- [x] POST `/epics/{id}/comments` - Create comment
- [x] GET `/wiki/{id}/comments` - Get wiki page comments
- [x] POST `/wiki/{id}/comments` - Create comment

## Attachments

- [x] GET `/attachments/{id}` - Get attachment by ID
- [x] PATCH `/attachments/{id}` - Update attachment
- [x] DELETE `/attachments/{id}` - Delete attachment

## History

- [x] GET `/history/{id}` - Get history entry by ID
- [x] GET `/history/{id}/comment` - Get history comment

## Applications

- [ ] GET `/applications` - List applications
- [ ] GET `/applications/{id}` - Get application by ID
- [ ] POST `/applications` - Create application
- [ ] PATCH `/applications/{id}` - Update application
- [ ] DELETE `/applications/{id}` - Delete application

## References

- [x] GET `/references/{projectId}/{ref}` - Get item by reference

## Statistics

- [x] GET `/stats/discover` - Discover statistics
- [x] GET `/stats/{projectId}` - Get project statistics

## Notes

- Endpoints marked with [x] are already implemented
- Endpoints marked with [ ] are not yet implemented
- This list is based on the Taiga REST API documentation
- Some endpoints may require specific permissions or project settings

