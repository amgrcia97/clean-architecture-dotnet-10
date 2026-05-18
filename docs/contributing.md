# Contributing

## Branch strategy

- Create feature branches using the pattern: `feature/<name>`
- Push changes to the feature branch
- The feature workflow will run tests and open or reuse a PR into `develop`

## Pull request flow

1. Open a PR from `feature/<name>` to `develop`
2. Wait for automated tests to pass
3. Use code review and approvals as required
4. Merge into `develop`
5. The develop-to-master workflow will open or reuse a PR from `develop` to `master`

## Best practices

- keep feature branches small and focused
- write meaningful commit messages
- update documentation when the architecture or workflow changes
- keep Docker Compose configuration stable for local development
