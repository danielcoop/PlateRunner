# PlateRunner Sample Site Integration Guide

This guide walks through installing PlateRunner into a fresh Umbraco project, configuring
the back-office document types, creating demo content for all three built-in themes, and
verifying mobile-friendly rendering.

---

## Prerequisites

| Requirement | Version |
|---|---|
| .NET SDK | 10.0+ |
| Umbraco CMS | 17.x |
| SQL Server or SQLite | Any supported |

Install the Umbraco project templates if you have not already:

```bash
dotnet new install Umbraco.Templates
```

---

## 1. Create the Umbraco project

```bash
dotnet new umbraco -n MySampleSite --friendly-name "Admin" --email "admin@example.com" \
    --password "SuperSecret123!" --development-storage-type SQLite
cd MySampleSite
```

> **Note:** SQLite is the easiest way to get started locally. Switch to SQL Server for
> production or multi-server deployments.

---

## 2. Add PlateRunner as a project reference

During active development, reference the package project directly.

```bash
dotnet add reference ../PlateRunner/src/PlateRunner.Package/PlateRunner.Package.csproj
```

When the package is published to NuGet, use the NuGet reference instead:

```bash
dotnet add package PlateRunner.Package
```

PlateRunner registers itself automatically via `PlateRunnerComposer`. No code changes are
needed in `Program.cs` or `Startup.cs`.

---

## 3. Run the site and complete installation

```bash
dotnet run
```

Open `https://localhost:5001/umbraco` and confirm Umbraco starts cleanly. The back-office
login uses the credentials provided during `dotnet new`.

> **Screenshot placeholder:** Back-office login screen showing successful startup.
> `docs/images/01-backoffice-login.png`

---

## 4. Create document types in the back-office

PlateRunner does not auto-install document types — editors create them in the back-office so
they can be tailored to their site structure. The aliases below must match exactly or the
mapper service will return empty/default values.

### 4.1 Microsite

**Settings → Document Types → Create**

| Field | Value |
|---|---|
| Name | Microsite |
| Alias | `microsite` |
| Icon | `icon-restaurant` |

Add a tab **Content** with these properties:

| Property name | Alias | Editor |
|---|---|---|
| Business Name | `businessName` | Textstring |
| Tagline | `tagline` | Textstring |
| Description | `description` | Textarea |
| Logo | `logo` | Media Picker |
| Hero Image | `heroImage` | Media Picker |
| Theme | `theme` | Dropdown (see §4.1a) |

Add a tab **Contact** with these properties:

| Property name | Alias | Editor |
|---|---|---|
| Phone | `contactPhone` | Textstring |
| Email | `contactEmail` | Textstring |
| Address Line 1 | `contactAddressLine1` | Textstring |
| Address Line 2 | `contactAddressLine2` | Textstring |
| City | `contactCity` | Textstring |
| Postcode | `contactPostcode` | Textstring |
| Country | `contactCountry` | Textstring |
| Website URL | `contactWebsiteUrl` | Textstring |

**Allowed child content types:**  
`menuSection`, `announcement`, `openingHour`, `socialLink`

#### 4.1a Configure the Theme dropdown

Create a **Data Type → Dropdown** named *PlateRunner Theme* with these pre-values:

| Label | Value |
|---|---|
| Minimal Elegant | `minimal-elegant` |
| Retro Diner | `retro-diner` |
| Fiesta Street | `fiesta-street` |

Use this data type for the `theme` property.

---

### 4.2 Menu Section

| Field | Value |
|---|---|
| Name | Menu Section |
| Alias | `menuSection` |
| Icon | `icon-list` |

Properties tab:

| Property name | Alias | Editor |
|---|---|---|
| Section Name | `sectionName` | Textstring |
| Section Description | `sectionDescription` | Textarea |

**Allowed child content types:** `menuItem`

---

### 4.3 Menu Item

| Field | Value |
|---|---|
| Name | Menu Item |
| Alias | `menuItem` |
| Icon | `icon-cutlery` |

Properties tab:

| Property name | Alias | Editor |
|---|---|---|
| Item Name | `itemName` | Textstring |
| Item Description | `itemDescription` | Textarea |
| Price | `price` | Decimal |
| Is Available | `isAvailable` | True/False |
| Dietary Tags | `dietaryTags` | Tags |
| Image | `image` | Media Picker |

---

### 4.4 Announcement

| Field | Value |
|---|---|
| Name | Announcement |
| Alias | `announcement` |
| Icon | `icon-megaphone` |

Properties tab:

| Property name | Alias | Editor |
|---|---|---|
| Title | `title` | Textstring |
| Body | `body` | Textarea |
| Call To Action Label | `callToActionLabel` | Textstring |
| Call To Action URL | `callToActionUrl` | Textstring |
| Is Visible | `isVisible` | True/False |

---

### 4.5 Opening Hour

| Field | Value |
|---|---|
| Name | Opening Hour |
| Alias | `openingHour` |
| Icon | `icon-time` |

Properties tab:

| Property name | Alias | Editor |
|---|---|---|
| Day | `day` | Textstring |
| Open Time | `openTime` | Textstring |
| Close Time | `closeTime` | Textstring |
| Is Closed | `isClosed` | True/False |
| Note | `note` | Textstring |

---

### 4.6 Social Link

| Field | Value |
|---|---|
| Name | Social Link |
| Alias | `socialLink` |
| Icon | `icon-share` |

Properties tab:

| Property name | Alias | Editor |
|---|---|---|
| Platform | `platform` | Textstring |
| URL | `url` | Textstring |

---

### 4.7 Allow microsite at root

Under **Settings → Document Types → Microsite → Permissions**, enable  
**Allow at root** so you can create microsite nodes at the top level of the content tree.

> **Screenshot placeholder:** Document types list in the back-office showing all six types.
> `docs/images/02-document-types.png`

---

## 5. Create demo microsite content

Create three microsite nodes in the **Content** section to demonstrate each theme.

---

### 5.1 Demo 1 — The Glass Table *(Minimal Elegant)*

**Content → Create → Microsite**

| Field | Value |
|---|---|
| Node name | The Glass Table |
| Business Name | The Glass Table |
| Tagline | Modern European dining |
| Description | An intimate dining room where seasonal ingredients meet quiet elegance. Tasting menus change weekly. |
| Theme | `minimal-elegant` |
| Phone | +44 20 7946 0123 |
| Email | reservations@glasstable.example |
| Address Line 1 | 12 Aldwych Lane |
| City | London |
| Postcode | WC2B 4EX |

**Child nodes:**

*Announcement → "Chef's Table Available"*

| Field | Value |
|---|---|
| Title | Chef's Table Available |
| Body | Join us for an exclusive four-course Chef's Table experience every Friday evening. Limited to 8 guests. |
| Call To Action Label | Book now |
| Call To Action URL | `#contact` |
| Is Visible | ✓ |

*Menu Section → "Starters"* with children:

| Item Name | Description | Price | Dietary Tags |
|---|---|---|---|
| Burrata & Heritage Tomato | Buffalo burrata, heirloom tomato, basil oil | 12.50 | vegetarian, gluten-free |
| Smoked Salmon Blinis | Hand-rolled blinis, crème fraîche, capers | 14.00 | |
| Charred Courgette Soup | Seasonal courgette, toasted pine nuts, aged parmesan | 9.50 | vegetarian |

*Menu Section → "Mains"* with children:

| Item Name | Description | Price | Dietary Tags |
|---|---|---|---|
| Pan-Seared Halibut | Pea purée, brown butter, crispy shallots | 32.00 | gluten-free |
| Duck Breast | Confit leg, cherry jus, pommes dauphine | 28.00 | |
| Wild Mushroom Risotto | Porcini, truffle oil, aged Parmesan | 22.00 | vegetarian, gluten-free |

*Menu Section → "Desserts"* with children:

| Item Name | Description | Price | Dietary Tags |
|---|---|---|---|
| Dark Chocolate Delice | Salted caramel, cocoa nib tuile | 9.00 | vegetarian |
| Lemon Posset | Shortbread, raspberry coulis | 8.50 | vegetarian |

*Opening Hours* (7 child nodes, one per day):

| Day | Open | Close | Is Closed |
|---|---|---|---|
| Monday | | | ✓ |
| Tuesday–Thursday | 18:00 | 22:00 | |
| Friday | 12:00 | 23:00 | |
| Saturday | 12:00 | 23:00 | |
| Sunday | 12:00 | 21:00 | |

*Social Links*:

| Platform | URL |
|---|---|
| instagram | https://instagram.com/glasstable |
| facebook | https://facebook.com/glasstable |

---

### 5.2 Demo 2 — Patty's Original Diner *(Retro Diner)*

**Content → Create → Microsite**

| Field | Value |
|---|---|
| Node name | Patty's Original Diner |
| Business Name | Patty's Original Diner |
| Tagline | Est. 1962 · Real burgers, real shakes |
| Description | Three generations of the Patty family serving the best smash burgers, crinkle fries, and hand-spun milkshakes in town. |
| Theme | `retro-diner` |
| Phone | 555-0182 |
| Email | hello@pattys.example |
| Address Line 1 | 88 Maple Drive |
| City | Springfield |

**Child nodes:**

*Announcement → "National Burger Day Special"*

| Field | Value |
|---|---|
| Title | 🍔 Buy One Get One Free! |
| Body | Celebrate National Burger Day with us. Any burger, any day this week! |
| Is Visible | ✓ |

*Menu Section → "Burgers"* with children:

| Item Name | Description | Price | Dietary Tags |
|---|---|---|---|
| The Original Patty | Double smash, American cheese, pickles, Patty sauce | 11.50 | |
| Smoky BBQ Stack | Triple patty, crispy bacon, BBQ sauce, coleslaw | 14.50 | |
| The Veggie Stack | Portobello mushroom, smashed avo, smoked cheddar | 11.00 | vegetarian |
| Chicken Ranch | Crispy buttermilk chicken, ranch dressing, lettuce | 12.00 | |

*Menu Section → "Sides"* with children:

| Item Name | Description | Price |
|---|---|---|
| Crinkle Fries | Seasoned, double-fried | 4.00 |
| Onion Rings | Beer-battered rings, chipotle dip | 4.50 |
| Mac 'n' Cheese Bites | Deep-fried mac, jalapeño jam | 5.50 |
| Side Salad | House greens, buttermilk dressing | 3.50 |

*Menu Section → "Shakes & Drinks"* with children:

| Item Name | Description | Price |
|---|---|---|
| Hand-Spun Vanilla Shake | Full-cream milk, real vanilla | 6.00 |
| Chocolate Peanut Butter Shake | Reese's pieces, whipped cream | 7.00 |
| Strawberry Shake | Fresh strawberry, housemade syrup | 6.00 |
| Classic Root Beer Float | Vintage root beer, vanilla ice cream | 5.50 |

*Opening Hours*:

| Day | Open | Close |
|---|---|---|
| Monday–Friday | 11:00 | 22:00 |
| Saturday | 10:00 | 23:00 |
| Sunday | 10:00 | 21:00 |

---

### 5.3 Demo 3 — El Fuego Taqueria *(Fiesta Street)*

**Content → Create → Microsite**

| Field | Value |
|---|---|
| Node name | El Fuego Taqueria |
| Business Name | El Fuego Taqueria |
| Tagline | 🌮 Authentic street tacos, every day |
| Description | Family recipes from Oaxaca. Made fresh, served fast, eaten loud. Find us at the Market Square Thursday to Sunday. |
| Theme | `fiesta-street` |
| Phone | 555-0247 |
| Email | hola@elfuego.example |
| Address Line 1 | Market Square |
| City | Austin |

**Child nodes:**

*Announcement → "New: Birria Tacos"*

| Field | Value |
|---|---|
| Title | 🔥 Birria Tacos Are Here! |
| Body | Slow-cooked beef birria with consommé for dipping. Limited daily — arrive early! |
| Call To Action Label | See the menu |
| Call To Action URL | `#menu` |
| Is Visible | ✓ |

*Menu Section → "🌮 Tacos"* with children:

| Item Name | Description | Price | Dietary Tags |
|---|---|---|---|
| Al Pastor | Spit-roasted pork, pineapple, cilantro, onion | 4.50 | gluten-free |
| Birria | Slow-cooked beef, consommé dip, oaxacan cheese | 5.50 | gluten-free |
| Grilled Fish | Baja battered fish, cabbage slaw, chipotle mayo | 5.00 | |
| Veggie Esquites | Corn, cotija, lime, chilli | 4.00 | vegetarian, gluten-free |
| Mushroom Tinga | Chipotle mushrooms, black beans, avocado crema | 4.50 | vegan, gluten-free |

*Menu Section → "🌯 Burritos"* with children:

| Item Name | Description | Price |
|---|---|---|
| El Clásico | Rice, beans, chicken tinga, salsa roja | 9.50 |
| El Grande | Double filling, guac, pico de gallo, sour cream | 12.00 |
| Veggie Supreme | Black beans, roasted peppers, avocado, chipotle salsa | 9.00 |

*Menu Section → "🥤 Drinks"* with children:

| Item Name | Description | Price |
|---|---|---|
| Horchata | House-made cinnamon rice water | 3.50 |
| Jamaica | Hibiscus iced tea, lime | 3.00 |
| Mexican Coke | Original glass bottle | 3.00 |

*Opening Hours*:

| Day | Open | Close | Is Closed | Note |
|---|---|---|---|---|
| Monday | | | ✓ | Closed |
| Tuesday | | | ✓ | Closed |
| Wednesday | | | ✓ | Closed |
| Thursday | 12:00 | 20:00 | | Market Square |
| Friday | 12:00 | 21:00 | | Market Square |
| Saturday | 11:00 | 22:00 | | Market Square |
| Sunday | 11:00 | 19:00 | | Market Square |

*Social Links*:

| Platform | URL |
|---|---|
| instagram | https://instagram.com/elfuegotaqueria |
| tiktok | https://tiktok.com/@elfuego |

> **Screenshot placeholder:** Content tree in the back-office showing all three microsite nodes.
> `docs/images/03-content-tree.png`

---

## 6. How rendering works

When Umbraco receives a request for a published `microsite` node, it routes the request to
`MicrositeController.Index()` (the render controller shipped with PlateRunner). The
controller:

1. Retrieves the current Umbraco page via `CurrentPage`
2. Calls `IMicrositeQueryService.GetMicrositeContent()` to walk up to the nearest microsite ancestor
3. Calls `IMicrositeMapper.Map()` to produce a `MicrositeViewModel`
4. Calls `IThemeResolver.Resolve()` (via the mapper) with the `theme` property value
5. Returns `View(theme.ViewPath, viewModel)` — the fully-qualified Razor path from the resolved `ThemeDefinition`

No template or document type template file needs to be created in the host site — the views
live inside the PlateRunner Razor Class Library and are served automatically.

---

## 7. Verify rendering

After publishing all three microsite nodes, open each URL in a browser.

### The Glass Table (Minimal Elegant)

```
https://localhost:5001/the-glass-table
```

Expected: Clean serif/sans layout, gold accent `#B8956A`, generous white space.

> **Screenshot placeholder:** The Glass Table rendered on desktop.
> `docs/images/04-minimal-elegant-desktop.png`

---

### Patty's Original Diner (Retro Diner)

```
https://localhost:5001/pattys-original-diner
```

Expected: Impact headlines, bold red `#C0392B`, checkerboard stripe header, dot-leader prices.

> **Screenshot placeholder:** Patty's Original Diner rendered on desktop.
> `docs/images/05-retro-diner-desktop.png`

---

### El Fuego Taqueria (Fiesta Street)

```
https://localhost:5001/el-fuego-taqueria
```

Expected: Gradient header, paper-picado bunting flags, warm orange/yellow palette, pill-style menu cards.

> **Screenshot placeholder:** El Fuego Taqueria rendered on desktop.
> `docs/images/06-fiesta-street-desktop.png`

---

## 8. Verify mobile-friendly rendering

PlateRunner views are built mobile-first. Test at common breakpoints using browser DevTools.

### Browser DevTools (Chrome / Edge)

1. Open the microsite URL
2. Press `F12` → click the **Toggle device toolbar** icon (or `Ctrl+Shift+M`)
3. Select a preset device (iPhone 14, Samsung Galaxy S23, iPad Mini)
4. Scroll through the full page and check:
   - Header and logo scale correctly
   - Menu items are readable without horizontal scroll
   - Price alignment is intact
   - Opening hours table doesn't overflow
   - CTA buttons are large enough to tap (min 44×44px)

### Recommended test viewports

| Device class | Width |
|---|---|
| Small phone | 375px |
| Large phone | 430px |
| Tablet portrait | 768px |
| Desktop | 1280px |

> **Screenshot placeholder:** El Fuego Taqueria at 375px (iPhone SE).
> `docs/images/07-fiesta-street-mobile.png`

> **Screenshot placeholder:** Patty's Original Diner at 430px (iPhone 15 Pro Max).
> `docs/images/08-retro-diner-mobile.png`

> **Screenshot placeholder:** The Glass Table at 768px (iPad Mini).
> `docs/images/09-minimal-elegant-tablet.png`

---

## 9. Switch themes

To change the theme for any microsite:

1. Open the microsite node in the back-office
2. Find the **Theme** dropdown on the **Content** tab
3. Select a different theme value (e.g. change from `minimal-elegant` to `retro-diner`)
4. Click **Save and Publish**
5. Reload the public URL — the layout, typography, and colours update immediately

No code changes are needed. All three built-in themes share the same `MicrositeViewModel`;
the theme value solely controls which view path and stylesheet are resolved.

---

## 10. Troubleshooting

### Page shows Umbraco's default "Page not found"

- Confirm the microsite node is published (green icon in the content tree)
- Confirm the `microsite` document type alias matches `PlateRunner.Package.Constants.ContentTypeAliases.Microsite` exactly (`microsite`, lowercase)
- Confirm the `MicrositeController` class name matches the document type alias (`MicrositeController` → `microsite`)

### Menu sections are empty

- Confirm `menuSection` child nodes exist directly under the microsite node
- Confirm the `sectionName` property alias is set (not `name`)
- Confirm the `menuItem` children have the `itemName` property filled in

### Theme is always Minimal Elegant

- Check the `theme` property value is one of: `minimal-elegant`, `retro-diner`, `fiesta-street`
- Values are compared case-insensitively; extra whitespace will cause fallback to default
- Check the data type pre-values match the values listed in §4.1a

### CSS is not loading

- Confirm the host project has `UseStaticFiles()` in the middleware pipeline (Umbraco adds this by default)
- The stylesheet path `/platerunner/css/themes/{theme}.css` is served from PlateRunner's `wwwroot` via the Razor Class Library static web asset pipeline

---

## 11. Next steps

- See [adding-themes.md](adding-themes.md) for instructions on creating custom themes
- See [backlog.md](backlog.md) for upcoming features and known gaps
- See [architecture.md](architecture.md) for the full service and rendering architecture
