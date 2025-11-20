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
- [ ] GET `/users/{id}` - Get user by ID
- [ ] GET `/users` - List users
- [ ] PATCH `/users/{id}` - Update user
- [ ] GET `/users/{id}/stats` - Get user statistics

## Projects

- [x] GET `/projects` - List projects
- [ ] GET `/projects/{id}` - Get project by ID
- [ ] POST `/projects` - Create project
- [ ] PATCH `/projects/{id}` - Update project
- [ ] DELETE `/projects/{id}` - Delete project
- [ ] GET `/projects/{id}/stats` - Get project statistics
- [ ] GET `/projects/{id}/modules` - Get project modules
- [ ] PATCH `/projects/{id}/modules` - Update project modules
- [ ] GET `/projects/{id}/memberships` - Get project memberships
- [ ] POST `/projects/{id}/memberships` - Create project membership
- [ ] GET `/projects/{id}/memberships/{membershipId}` - Get membership
- [ ] PATCH `/projects/{id}/memberships/{membershipId}` - Update membership
- [ ] DELETE `/projects/{id}/memberships/{membershipId}` - Delete membership
- [ ] GET `/projects/{id}/roles` - Get project roles
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
- [ ] GET `/userstories/{id}` - Get user story by ID
- [ ] POST `/userstories` - Create user story
- [ ] PATCH `/userstories/{id}` - Update user story
- [ ] DELETE `/userstories/{id}` - Delete user story
- [ ] GET `/userstories/{id}/attachments` - Get user story attachments
- [ ] POST `/userstories/{id}/attachments` - Upload attachment
- [ ] GET `/userstories/{id}/attachments/{attachmentId}` - Get attachment
- [ ] DELETE `/userstories/{id}/attachments/{attachmentId}` - Delete attachment
- [ ] GET `/userstories/{id}/history` - Get user story history
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

- [ ] GET `/tasks` - List tasks
- [ ] GET `/tasks/{id}` - Get task by ID
- [ ] POST `/tasks` - Create task
- [ ] PATCH `/tasks/{id}` - Update task
- [ ] DELETE `/tasks/{id}` - Delete task
- [ ] GET `/tasks/{id}/attachments` - Get task attachments
- [ ] POST `/tasks/{id}/attachments` - Upload attachment
- [ ] GET `/tasks/{id}/attachments/{attachmentId}` - Get attachment
- [ ] DELETE `/tasks/{id}/attachments/{attachmentId}` - Delete attachment
- [ ] GET `/tasks/{id}/history` - Get task history
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

- [ ] GET `/issues` - List issues
- [ ] GET `/issues/{id}` - Get issue by ID
- [ ] POST `/issues` - Create issue
- [ ] PATCH `/issues/{id}` - Update issue
- [ ] DELETE `/issues/{id}` - Delete issue
- [ ] GET `/issues/{id}/attachments` - Get issue attachments
- [ ] POST `/issues/{id}/attachments` - Upload attachment
- [ ] GET `/issues/{id}/attachments/{attachmentId}` - Get attachment
- [ ] DELETE `/issues/{id}/attachments/{attachmentId}` - Delete attachment
- [ ] GET `/issues/{id}/history` - Get issue history
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

- [ ] GET `/epics` - List epics
- [ ] GET `/epics/{id}` - Get epic by ID
- [ ] POST `/epics` - Create epic
- [ ] PATCH `/epics/{id}` - Update epic
- [ ] DELETE `/epics/{id}` - Delete epic
- [ ] GET `/epics/{id}/attachments` - Get epic attachments
- [ ] POST `/epics/{id}/attachments` - Upload attachment
- [ ] GET `/epics/{id}/attachments/{attachmentId}` - Get attachment
- [ ] DELETE `/epics/{id}/attachments/{attachmentId}` - Delete attachment
- [ ] GET `/epics/{id}/history` - Get epic history
- [ ] GET `/epics/{id}/related-userstories` - Get related user stories
- [ ] POST `/epics/{id}/related-userstories` - Add related user story
- [ ] DELETE `/epics/{id}/related-userstories/{userStoryId}` - Remove related user story

## Milestones (Sprints)

- [ ] GET `/milestones` - List milestones
- [ ] GET `/milestones/{id}` - Get milestone by ID
- [ ] POST `/milestones` - Create milestone
- [ ] PATCH `/milestones/{id}` - Update milestone
- [ ] DELETE `/milestones/{id}` - Delete milestone
- [ ] GET `/milestones/{id}/stats` - Get milestone statistics
- [ ] GET `/milestones/{id}/burndown` - Get burndown chart data
- [ ] GET `/milestones/{id}/userstories` - Get milestone user stories
- [ ] POST `/milestones/{id}/userstories` - Add user story to milestone
- [ ] DELETE `/milestones/{id}/userstories/{userStoryId}` - Remove user story from milestone

## Wiki

- [ ] GET `/wiki` - List wiki pages
- [ ] GET `/wiki/{id}` - Get wiki page by ID
- [ ] POST `/wiki` - Create wiki page
- [ ] PATCH `/wiki/{id}` - Update wiki page
- [ ] DELETE `/wiki/{id}` - Delete wiki page
- [ ] GET `/wiki/{id}/attachments` - Get wiki page attachments
- [ ] POST `/wiki/{id}/attachments` - Upload attachment
- [ ] GET `/wiki/{id}/attachments/{attachmentId}` - Get attachment
- [ ] DELETE `/wiki/{id}/attachments/{attachmentId}` - Delete attachment
- [ ] GET `/wiki/{id}/history` - Get wiki page history

## Webhooks

- [ ] GET `/webhooks` - List webhooks
- [ ] GET `/webhooks/{id}` - Get webhook by ID
- [ ] POST `/webhooks` - Create webhook
- [ ] PATCH `/webhooks/{id}` - Update webhook
- [ ] DELETE `/webhooks/{id}` - Delete webhook
- [ ] POST `/webhooks/{id}/test` - Test webhook
- [ ] GET `/webhooks/{id}/logs` - Get webhook logs

## Notifications

- [ ] GET `/notifications` - List notifications
- [ ] GET `/notifications/{id}` - Get notification by ID
- [ ] PATCH `/notifications/{id}` - Mark notification as read
- [ ] PATCH `/notifications/read` - Mark all notifications as read
- [ ] GET `/notifications/unread` - Get unread notifications count

## Search

- [ ] GET `/search` - Search across projects
- [ ] GET `/search/{projectId}` - Search within project

## Activity

- [ ] GET `/timeline/{projectId}` - Get project timeline
- [ ] GET `/timeline/{projectId}/profile` - Get profile timeline
- [ ] GET `/timeline/{projectId}/user/{userId}` - Get user timeline

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

- [ ] GET `/severities` - List severities
- [ ] GET `/priorities` - List priorities
- [ ] GET `/issue-statuses` - List issue statuses
- [ ] GET `/issue-types` - List issue types
- [ ] GET `/task-statuses` - List task statuses
- [ ] GET `/userstory-statuses` - List user story statuses

## Comments

- [ ] GET `/userstories/{id}/comments` - Get user story comments
- [ ] POST `/userstories/{id}/comments` - Create comment
- [ ] GET `/tasks/{id}/comments` - Get task comments
- [ ] POST `/tasks/{id}/comments` - Create comment
- [ ] GET `/issues/{id}/comments` - Get issue comments
- [ ] POST `/issues/{id}/comments` - Create comment
- [ ] GET `/epics/{id}/comments` - Get epic comments
- [ ] POST `/epics/{id}/comments` - Create comment
- [ ] GET `/wiki/{id}/comments` - Get wiki page comments
- [ ] POST `/wiki/{id}/comments` - Create comment

## Attachments

- [ ] GET `/attachments/{id}` - Get attachment by ID
- [ ] PATCH `/attachments/{id}` - Update attachment
- [ ] DELETE `/attachments/{id}` - Delete attachment

## History

- [ ] GET `/history/{id}` - Get history entry by ID
- [ ] GET `/history/{id}/comment` - Get history comment

## Applications

- [ ] GET `/applications` - List applications
- [ ] GET `/applications/{id}` - Get application by ID
- [ ] POST `/applications` - Create application
- [ ] PATCH `/applications/{id}` - Update application
- [ ] DELETE `/applications/{id}` - Delete application

## References

- [ ] GET `/references/{projectId}/{ref}` - Get item by reference

## Statistics

- [ ] GET `/stats/discover` - Discover statistics
- [ ] GET `/stats/{projectId}` - Get project statistics

## Notes

- Endpoints marked with [x] are already implemented
- Endpoints marked with [ ] are not yet implemented
- This list is based on the Taiga REST API documentation
- Some endpoints may require specific permissions or project settings

