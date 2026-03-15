# GitHub Copilot Instructions

You are helping build an open-source Umbraco package called **PlateRunner** for hospitality microsites.

## Product Overview

**PlateRunner** is an installable Umbraco package/plugin that enables small restaurants and food trucks to create branded microsites with editable menus.

The package should support:

- microsite page generation
- menu sections and menu items
- specials / announcements
- opening hours
- contact details
- social links
- QR-friendly public pages
- selectable visual themes

This package is intended to be:

1. open source
2. installable into an Umbraco project
3. useful for developers, agencies, and self-hosters
4. a foundation for a separate paid hosted offering

## Technical Stack

Build for:

- .NET
- C#
- ASP.NET Core
- Umbraco CMS
- Razor views
- HTML/CSS/JavaScript

Prefer compatibility with a single target Umbraco major version first, and keep version assumptions explicit in code and docs.

## Product Goals

The package should let an Umbraco site owner:

- install the package
- create a hospitality microsite
- manage menu content in Umbraco
- choose from a small set of built-in themes
- publish a mobile-friendly public-facing microsite

## Core Concepts

Model the following concepts:

- Microsite
- Theme
- Menu Section
- Menu Item
- Announcement / Special
- Opening Hours
- Contact Details
- Social Links

The package should be suitable for:

- small restaurants
- food trucks
- pop-ups
- takeaways
- cafes

## Package Philosophy

This is an open-source package, not a locked-down SaaS product.

- core functionality must work fully in the open-source package
- do not design artificial paywalls into the package
- hosted/SaaS concerns should remain separate from the package core
- monetization should come later from convenience, hosting, support, or premium extras, not from crippling the OSS package

## Architectural Guidance

Prefer these patterns:

- use standard Umbraco package conventions
- dependency injection for services
- interfaces for service boundaries
- view models for rendering
- keep business logic out of Razor views
- keep themes modular
- prefer simple, maintainable solutions over clever abstractions
- use Umbraco-native concepts wherever possible

## Theme System

Support built-in themes:

- `minimal-elegant`
- `retro-diner`
- `fiesta-street`

Theme support should:

- be data-driven
- be easy to extend
- allow new themes with minimal code changes
- keep shared rendering logic reusable

## CMS / Editor Experience

Editors should be able to manage:

- business name, tagline, description
- logo and hero image
- featured message
- menu sections and items, prices, availability
- dietary tags
- contact details
- opening hours
- social links
- selected theme

Keep the editor experience simple and practical.

## Rendering Requirements

Public microsites should be:

- mobile-first
- fast-loading
- accessible
- QR-friendly
- easy to scan and read on phones
- visually distinct by theme

## Coding Standards

- clear naming
- classes focused on single responsibilities
- small services over bloated controllers
- view models for rendering
- async where appropriate
- null handling explicit and safe
- minimal backoffice customization in v1

## Package Structure

```text
/src
  /PlateRunner.Package
    /Constants
    /Extensions
    /Services
      /Interfaces
      /Implementations
    /ViewModels
    /Views
      /Partials
      /Themes
        /MinimalElegant
        /RetroDiner
        /FiestaStreet
    /wwwroot
      /css
        /themes
      /js
    /App_Plugins
/samples
  /UmbracoSampleSite
/docs