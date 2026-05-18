# GitHub Actions Workflows

This project includes two automated workflows under `.github/workflows/`.

## Feature branch workflow

File: `.github/workflows/feature-branch-pr.yml`

- triggers on `push` for branches starting with `feature`
- restores and tests `Ecommerce.slnx`
- if tests pass, creates or reuses an open PR into `develop`

## Develop-to-master workflow

File: `.github/workflows/develop-to-master-pr.yml`

- triggers when a pull request targeting `develop` is closed and merged
- creates or reuses an open PR from `develop` to `master`

## Repository secret for PR creation

If Actions cannot create PRs with the default token, configure a repository secret named `PR_TOKEN` containing a Personal Access Token with repository write permissions.

## Recommended branch protection

Protect `develop` and `master` with rules that:

- require pull requests before merging
- require status checks to pass
- prevent force pushes
- prevent branch deletion
- optionally require code reviews and approvals
