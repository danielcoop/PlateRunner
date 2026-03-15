# PlateRunner Architecture

## Overview

PlateRunner is an open-source Umbraco package that provides microsite functionality for small restaurants, food trucks, and cafes.

The package adds:

- Mobile-friendly branded microsites
- Editable menus and specials
- Opening hours, contact, and social links
- Themeable templates
- QR-friendly public pages

## Core Principles

- Package-first architecture
- Open-source friendly
- Simple editor experience
- Clear separation of content, mapping, and rendering
- Mobile-first public pages
- Extensible theme system
- No SaaS assumptions in the package core

## Main Responsibilities

- Hospitality-focused content structures
- Rendering pipeline for public microsites
- Built-in themes
- Shared partials for menu display
- Helper services for theme resolution and mapping
- Sample/demo implementation

## Content Areas

- Branding/hero
- Featured announcements or specials
- Menu sections
- Menu items
- Opening hours
- Contact details
- Social links

## Rendering Flow

1. Umbraco content configured for microsite
2. Query service retrieves content
3. Mapper converts content into view models
4. Theme resolver selects theme
5. Shared partials and theme views render public page

## Theme Model

- Key
- Display name
- View path
- Stylesheet path
- Support for built-in and future themes

## Services

- `IThemeResolver`
- `IMicrositeMapper`
- `IMicrositeQueryService`
- `IPriceFormatter`

## Packaging

- Installable, useful OSS package
- Hosted functionality separate
- Focus on installability, clarity, and maintainability

## Sample Site

- Demonstrates installation and built-in themes
- Includes demo businesses
- Reference implementation for contributors and users

## Future Extensions

- Additional themes
- Multilingual support
- Richer announcements
- Content import tools
- Premium theme packs
- Hosted deployment experience