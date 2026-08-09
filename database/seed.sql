-- ============================================================
-- DevPulse - Initial Database Seed
-- PostgreSQL
-- ============================================================

BEGIN;

-- ============================================================
-- CONSTANT UUIDs
-- ============================================================

-- Team Members
-- 1  = 11111111-1111-1111-1111-111111111111
-- 2  = 22222222-2222-2222-2222-222222222222
-- ...
-- 10 = aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa

-- ============================================================
-- TEAM MEMBERS
-- ============================================================

INSERT INTO "TeamMembers"
(
    "Id",
    "Name",
    "Email",
    "Role",
    "AuditRecord_CreatedAt",
    "AuditRecord_LastModifiedAt",
    "AuditRecord_CreatedBy",
    "AuditRecord_LastModifiedBy"
)
VALUES
    (
        '11111111-1111-1111-1111-111111111111',
        'Hector Hernandez',
        'hector.h@devpulse.local',
        0,
        '2026-08-01 09:00:00',
        '2026-08-01 09:00:00',
        'system',
        'system'
    ),
    (
        '22222222-2222-2222-2222-222222222222',
        'Carlos Mendoza',
        'carlos.m@devpulse.local',
        1,
        '2026-08-01 09:05:00',
        '2026-08-01 09:05:00',
        'system',
        'system'
    ),
    (
        '33333333-3333-3333-3333-333333333333',
        'Maria Rodriguez',
        'maria.r@devpulse.local',
        2,
        '2026-08-01 09:10:00',
        '2026-08-01 09:10:00',
        'system',
        'system'
    ),
    (
        '44444444-4444-4444-4444-444444444444',
        'Luis Garcia',
        'luis.g@devpulse.local',
        0,
        '2026-08-01 09:15:00',
        '2026-08-01 09:15:00',
        'system',
        'system'
    ),
    (
        '55555555-5555-5555-5555-555555555555',
        'Ana Torres',
        'ana.t@devpulse.local',
        1,
        '2026-08-01 09:20:00',
        '2026-08-01 09:20:00',
        'system',
        'system'
    ),
    (
        '66666666-6666-6666-6666-666666666666',
        'Diego Flores',
        'diego.f@devpulse.local',
        2,
        '2026-08-01 09:25:00',
        '2026-08-01 09:25:00',
        'system',
        'system'
    ),
    (
        '77777777-7777-7777-7777-777777777777',
        'Sofia Vargas',
        'sofia.v@devpulse.local',
        0,
        '2026-08-01 09:30:00',
        '2026-08-01 09:30:00',
        'system',
        'system'
    ),
    (
        '88888888-8888-8888-8888-888888888888',
        'Miguel Castro',
        'miguel.c@devpulse.local',
        1,
        '2026-08-01 09:35:00',
        '2026-08-01 09:35:00',
        'system',
        'system'
    ),
    (
        '99999999-9999-9999-9999-999999999999',
        'Laura Ramirez',
        'laura.r@devpulse.local',
        2,
        '2026-08-01 09:40:00',
        '2026-08-01 09:40:00',
        'system',
        'system'
    ),
    (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa',
        'Jorge Castillo',
        'jorge.c@devpulse.local',
        0,
        '2026-08-01 09:45:00',
        '2026-08-01 09:45:00',
        'system',
        'system'
    );

-- ============================================================
-- INCIDENTS
-- 2 incidents per team member
-- ============================================================

INSERT INTO "Incidents"
(
    "Id",
    "Title",
    "Description",
    "ScreenshotUrl",
    "Recommendation",
    "Severity",
    "Status",
    "ReportedAt",
    "ExpectedResolutionAt",
    "ResolvedAt",
    "AssignedToId",
    "AuditRecord_CreatedAt",
    "AuditRecord_LastModifiedAt",
    "AuditRecord_CreatedBy",
    "AuditRecord_LastModifiedBy"
)
VALUES

-- Hector
(
    'a1000001-0001-0001-0001-000000000001',
    'API authentication failure',
    'Users are intermittently receiving authentication errors when accessing the API.',
    'https://example.com/screenshots/api-auth.png',
    'Review token validation and authentication middleware logs.',
    2,
    1,
    '2026-08-02 08:30:00',
    '2026-08-03 08:30:00',
    NULL,
    '11111111-1111-1111-1111-111111111111',
    '2026-08-02 08:30:00',
    '2026-08-02 08:30:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000002',
    'Slow database queries',
    'Several endpoints are experiencing increased response times due to slow database queries.',
    'https://example.com/screenshots/database.png',
    'Review query execution plans and add indexes where appropriate.',
    1,
    2,
    '2026-08-03 10:15:00',
    '2026-08-03 14:15:00',
    NULL,
    '11111111-1111-1111-1111-111111111111',
    '2026-08-03 10:15:00',
    '2026-08-03 11:00:00',
    'system',
    'system'
),

-- Carlos
(
    'a1000001-0001-0001-0001-000000000003',
    'Payment service unavailable',
    'The payment service is returning errors for a subset of transactions.',
    'https://example.com/screenshots/payment.png',
    'Check external payment provider availability and retry policies.',
    3,
    2,
    '2026-08-03 12:00:00',
    '2026-08-03 13:00:00',
    NULL,
    '22222222-2222-2222-2222-222222222222',
    '2026-08-03 12:00:00',
    '2026-08-03 12:30:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000004',
    'Incorrect HTTP status code',
    'The API returns HTTP 500 instead of HTTP 404 for missing resources.',
    'https://example.com/screenshots/http-status.png',
    'Review exception handling and resource-not-found behavior.',
    0,
    0,
    '2026-08-04 09:00:00',
    '2026-08-07 09:00:00',
    NULL,
    '22222222-2222-2222-2222-222222222222',
    '2026-08-04 09:00:00',
    '2026-08-04 09:00:00',
    'system',
    'system'
),

-- Maria
(
    'a1000001-0001-0001-0001-000000000005',
    'Email notification delay',
    'System notification emails are being delivered several minutes after the triggering event.',
    'https://example.com/screenshots/email-delay.png',
    'Review the background worker and message queue configuration.',
    1,
    3,
    '2026-08-04 11:30:00',
    '2026-08-04 15:30:00',
    '2026-08-04 14:45:00',
    '33333333-3333-3333-3333-333333333333',
    '2026-08-04 11:30:00',
    '2026-08-04 14:45:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000006',
    'Dashboard metrics missing',
    'Some dashboard metrics are not being displayed after deployment.',
    'https://example.com/screenshots/dashboard.png',
    'Verify metric aggregation and cache invalidation.',
    2,
    1,
    '2026-08-05 08:00:00',
    '2026-08-06 08:00:00',
    NULL,
    '33333333-3333-3333-3333-333333333333',
    '2026-08-05 08:00:00',
    '2026-08-05 08:00:00',
    'system',
    'system'
),

-- Luis
(
    'a1000001-0001-0001-0001-000000000007',
    'Memory consumption increase',
    'Application memory usage has increased continuously during the last deployment.',
    'https://example.com/screenshots/memory.png',
    'Inspect object allocations and long-lived service dependencies.',
    2,
    2,
    '2026-08-05 10:00:00',
    '2026-08-06 10:00:00',
    NULL,
    '44444444-4444-4444-4444-444444444444',
    '2026-08-05 10:00:00',
    '2026-08-05 11:00:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000008',
    'Frontend asset loading failure',
    'Static assets fail to load for users after the latest deployment.',
    'https://example.com/screenshots/assets.png',
    'Review static file configuration and deployment artifact paths.',
    1,
    3,
    '2026-08-05 13:00:00',
    '2026-08-05 17:00:00',
    '2026-08-05 16:20:00',
    '44444444-4444-4444-4444-444444444444',
    '2026-08-05 13:00:00',
    '2026-08-05 16:20:00',
    'system',
    'system'
),

-- Ana
(
    'a1000001-0001-0001-0001-000000000009',
    'Database connection pool exhaustion',
    'The application is temporarily unable to acquire database connections.',
    'https://example.com/screenshots/pool.png',
    'Review connection lifetime and ensure database connections are disposed correctly.',
    3,
    3,
    '2026-08-06 08:15:00',
    '2026-08-06 09:15:00',
    '2026-08-06 09:00:00',
    '55555555-5555-5555-5555-555555555555',
    '2026-08-06 08:15:00',
    '2026-08-06 09:00:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000010',
    'Invalid cache entries',
    'Cached responses contain stale information after data updates.',
    'https://example.com/screenshots/cache.png',
    'Review cache invalidation rules for updated resources.',
    0,
    1,
    '2026-08-06 10:30:00',
    '2026-08-09 10:30:00',
    NULL,
    '55555555-5555-5555-5555-555555555555',
    '2026-08-06 10:30:00',
    '2026-08-06 10:30:00',
    'system',
    'system'
),

-- Diego
(
    'a1000001-0001-0001-0001-000000000011',
    'Background job failure',
    'Scheduled background jobs are failing unexpectedly.',
    'https://example.com/screenshots/background-job.png',
    'Review worker logs and retry configuration.',
    2,
    2,
    '2026-08-06 12:00:00',
    '2026-08-07 12:00:00',
    NULL,
    '66666666-6666-6666-6666-666666666666',
    '2026-08-06 12:00:00',
    '2026-08-06 12:15:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000012',
    'Incorrect user permissions',
    'Some users are receiving access to resources outside their assigned role.',
    'https://example.com/screenshots/permissions.png',
    'Review authorization policies and role mappings.',
    3,
    1,
    '2026-08-07 09:00:00',
    '2026-08-07 10:00:00',
    NULL,
    '66666666-6666-6666-6666-666666666666',
    '2026-08-07 09:00:00',
    '2026-08-07 09:00:00',
    'system',
    'system'
),

-- Sofia
(
    'a1000001-0001-0001-0001-000000000013',
    'API rate limiting issue',
    'Rate limiting is rejecting legitimate requests during normal traffic.',
    'https://example.com/screenshots/rate-limit.png',
    'Review rate-limit thresholds and client identification.',
    1,
    3,
    '2026-08-07 11:00:00',
    '2026-08-07 15:00:00',
    '2026-08-07 13:30:00',
    '77777777-7777-7777-7777-777777777777',
    '2026-08-07 11:00:00',
    '2026-08-07 13:30:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000014',
    'Missing audit information',
    'Some records are being created without complete audit information.',
    'https://example.com/screenshots/audit.png',
    'Review audit record initialization and persistence.',
    2,
    0,
    '2026-08-07 14:00:00',
    '2026-08-08 14:00:00',
    NULL,
    '77777777-7777-7777-7777-777777777777',
    '2026-08-07 14:00:00',
    '2026-08-07 14:00:00',
    'system',
    'system'
),

-- Miguel
(
    'a1000001-0001-0001-0001-000000000015',
    'Search endpoint timeout',
    'The search endpoint occasionally exceeds the configured timeout.',
    'https://example.com/screenshots/search.png',
    'Optimize the search query and review database indexes.',
    2,
    2,
    '2026-08-08 08:00:00',
    '2026-08-09 08:00:00',
    NULL,
    '88888888-8888-8888-8888-888888888888',
    '2026-08-08 08:00:00',
    '2026-08-08 08:15:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000016',
    'Logging service failure',
    'Application logs are not reaching the centralized logging service.',
    'https://example.com/screenshots/logging.png',
    'Review logging transport configuration and connectivity.',
    1,
    3,
    '2026-08-08 10:00:00',
    '2026-08-08 14:00:00',
    '2026-08-08 12:00:00',
    '88888888-8888-8888-8888-888888888888',
    '2026-08-08 10:00:00',
    '2026-08-08 12:00:00',
    'system',
    'system'
),

-- Laura
(
    'a1000001-0001-0001-0001-000000000017',
    'Configuration not loaded',
    'A production configuration value is not being loaded correctly.',
    'https://example.com/screenshots/config.png',
    'Review environment variables and configuration providers.',
    1,
    1,
    '2026-08-08 12:00:00',
    '2026-08-08 16:00:00',
    NULL,
    '99999999-9999-9999-9999-999999999999',
    '2026-08-08 12:00:00',
    '2026-08-08 12:00:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000018',
    'File upload validation error',
    'Valid files are occasionally rejected by the upload validation process.',
    'https://example.com/screenshots/upload.png',
    'Review MIME type validation and file extension handling.',
    0,
    0,
    '2026-08-08 13:00:00',
    '2026-08-11 13:00:00',
    NULL,
    '99999999-9999-9999-9999-999999999999',
    '2026-08-08 13:00:00',
    '2026-08-08 13:00:00',
    'system',
    'system'
),

-- Jorge
(
    'a1000001-0001-0001-0001-000000000019',
    'Deployment health check failure',
    'The deployment health check reports failures after a new release.',
    'https://example.com/screenshots/health-check.png',
    'Review startup dependencies and health check configuration.',
    2,
    3,
    '2026-08-08 15:00:00',
    '2026-08-09 15:00:00',
    '2026-08-08 18:00:00',
    'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa',
    '2026-08-08 15:00:00',
    '2026-08-08 18:00:00',
    'system',
    'system'
),
(
    'a1000001-0001-0001-0001-000000000020',
    'Unexpected API response format',
    'An API endpoint returns an unexpected response structure to some clients.',
    'https://example.com/screenshots/api-response.png',
    'Review response DTO mapping and serialization configuration.',
    0,
    0,
    '2026-08-09 08:00:00',
    '2026-08-12 08:00:00',
    NULL,
    'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa',
    '2026-08-09 08:00:00',
    '2026-08-09 08:00:00',
    'system',
    'system'
);

-- ============================================================
-- POST MORTEMS
-- 10 total
-- First incident of each TeamMember
-- ============================================================

INSERT INTO "PostMortems"
(
    "Id",
    "RootCause",
    "LessonsLearned",
    "IncidentId",
    "AuditRecord_CreatedAt",
    "AuditRecord_LastModifiedAt",
    "AuditRecord_CreatedBy",
    "AuditRecord_LastModifiedBy"
)
VALUES
    (
        'b1000001-0001-0001-0001-000000000001',
        'The authentication middleware was using an outdated token validation configuration.',
        'Authentication configuration should be validated automatically during deployment.',
        'a1000001-0001-0001-0001-000000000001',
        '2026-08-04 10:00:00',
        '2026-08-04 10:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000002',
        'The payment provider experienced intermittent connectivity issues.',
        'External dependencies require retries, timeouts and monitoring.',
        'a1000001-0001-0001-0001-000000000003',
        '2026-08-04 15:00:00',
        '2026-08-04 15:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000003',
        'The background email worker accumulated messages faster than it could process them.',
        'Queue depth should be monitored and worker capacity should scale with demand.',
        'a1000001-0001-0001-0001-000000000005',
        '2026-08-05 09:00:00',
        '2026-08-05 09:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000004',
        'A memory-intensive operation was introduced during the latest deployment.',
        'Memory usage should be monitored as part of deployment validation.',
        'a1000001-0001-0001-0001-000000000007',
        '2026-08-06 10:00:00',
        '2026-08-06 10:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000005',
        'Database connections were not released correctly by a long-running operation.',
        'Connection lifetime should be monitored and integration tests should cover resource disposal.',
        'a1000001-0001-0001-0001-000000000009',
        '2026-08-06 11:00:00',
        '2026-08-06 11:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000006',
        'A background worker failed to handle an unexpected exception.',
        'Background jobs should have structured retries and failure monitoring.',
        'a1000001-0001-0001-0001-000000000011',
        '2026-08-07 14:00:00',
        '2026-08-07 14:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000007',
        'The rate limiter used a threshold that was too restrictive for normal traffic.',
        'Rate-limit policies should be based on real traffic patterns.',
        'a1000001-0001-0001-0001-000000000013',
        '2026-08-07 16:00:00',
        '2026-08-07 16:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000008',
        'The search query performed a full table scan for certain filters.',
        'Frequently used search filters should have appropriate database indexes.',
        'a1000001-0001-0001-0001-000000000015',
        '2026-08-08 16:00:00',
        '2026-08-08 16:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000009',
        'The production configuration provider was missing a required environment variable.',
        'Required configuration should be validated during application startup.',
        'a1000001-0001-0001-0001-000000000017',
        '2026-08-09 09:00:00',
        '2026-08-09 09:00:00',
        'system',
        'system'
    ),
    (
        'b1000001-0001-0001-0001-000000000010',
        'The deployment health check depended on a service that was not yet ready.',
        'Health checks should distinguish between application startup and dependency readiness.',
        'a1000001-0001-0001-0001-000000000019',
        '2026-08-09 10:00:00',
        '2026-08-09 10:00:00',
        'system',
        'system'
    );

COMMIT;