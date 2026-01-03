# Design System - Online Learning Platform

## Overview
Modern, professional design system with clean aesthetics and excellent user experience.

## Color Palette

### Primary Colors
- **Primary Blue**: `#2563eb` - Main brand color, used for CTAs and interactive elements
- **Secondary Purple**: `#7c3aed` - Accent color for gradients and highlights
- **Accent Cyan**: `#06b6d4` - Used for special highlights

### Semantic Colors
- **Success**: `#10b981` (Green) - Success states, confirmations
- **Warning**: `#f59e0b` (Amber) - Warnings, cautions
- **Danger**: `#ef4444` (Red) - Errors, destructive actions
- **Info**: `#3b82f6` (Blue) - Informational messages

### Neutral Colors
- **Gray 50**: `#f9fafb` - Lightest background
- **Gray 100**: `#f3f4f6` - Light background
- **Gray 200**: `#e5e7eb` - Borders, dividers
- **Gray 300**: `#d1d5db` - Disabled states
- **Gray 400**: `#9ca3af` - Placeholder text
- **Gray 500**: `#6b7280` - Secondary text
- **Gray 600**: `#4b5563` - Body text
- **Gray 700**: `#374151` - Labels
- **Gray 800**: `#1f2937` - Headings
- **Gray 900**: `#111827` - Primary text
- **White**: `#ffffff` - Cards, backgrounds

## Typography

### Font Family
- **Primary**: `'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif`
- **Fallback**: System fonts for optimal performance

### Heading Sizes
- **H1**: `2.5rem (40px)` - Page titles
- **H2**: `2rem (32px)` - Section headers
- **H3**: `1.75rem (28px)` - Subsection headers
- **H4**: `1.5rem (24px)` - Card titles
- **H5**: `1.25rem (20px)` - Small headings
- **H6**: `1rem (16px)` - Smallest headings

### Body Text
- **Base**: `16px` - Standard body text
- **Line Height**: `1.6` - Comfortable reading

## Spacing Scale

Consistent spacing using multiples of 4px:
- **xs**: `4px` - Tight spacing
- **sm**: `8px` - Small gaps
- **md**: `16px` - Default spacing
- **lg**: `24px` - Section spacing
- **xl**: `32px` - Large sections
- **2xl**: `48px` - Major sections
- **3xl**: `64px` - Page sections

## Shadows

Layered depth system:
- **xs**: `0 1px 2px 0 rgba(0, 0, 0, 0.05)` - Subtle elevation
- **sm**: `0 1px 3px 0 rgba(0, 0, 0, 0.1)` - Small cards
- **md**: `0 4px 6px -1px rgba(0, 0, 0, 0.1)` - Cards
- **lg**: `0 10px 15px -3px rgba(0, 0, 0, 0.1)` - Modals
- **xl**: `0 20px 25px -5px rgba(0, 0, 0, 0.1)` - Popovers
- **2xl**: `0 25px 50px -12px rgba(0, 0, 0, 0.25)` - Major elements

## Border Radius
- **Default**: `8px` - Standard rounded corners
- **Large**: `12px` - Cards, modals
- **XL**: `16px` - Hero sections

## Components

### Buttons
- **Primary**: Blue background with white text
- **Secondary**: Outline with primary color
- **Success**: Green for positive actions
- **Danger**: Red for destructive actions
- **Hover**: 2px lift with enhanced shadow
- **Focus**: 3px ring in primary color

### Cards
- **Background**: White
- **Border**: 1px solid gray-200
- **Shadow**: Subtle elevation (shadow-sm)
- **Hover**: Lift 6px with enhanced shadow
- **Border radius**: 12px

### Forms
- **Inputs**: 1px border, 8px radius
- **Focus**: Blue ring (3px), border color change
- **Placeholder**: Gray-400
- **Label**: Gray-700, 14px

### Navigation
- **Background**: White with backdrop blur
- **Shadow**: Subtle border shadow
- **Links**: Gray-600 with blue hover
- **Active**: Blue background with opacity

## Gradients

Used sparingly for visual interest:
- **Primary Gradient**: `linear-gradient(135deg, #2563eb 0%, #7c3aed 100%)`
- **Hero Background**: `linear-gradient(135deg, #1e40af 0%, #7c3aed 100%)`
- **Subtle Overlay**: `linear-gradient(135deg, rgba(37, 99, 235, 0.05) 0%, rgba(124, 58, 237, 0.05) 100%)`

## Transitions

Smooth, consistent animations:
- **Duration**: `200ms-300ms` for most interactions
- **Easing**: `cubic-bezier(0.4, 0, 0.2, 1)` - Material Design easing
- **Properties**: `transform`, `box-shadow`, `opacity`, `background`, `border-color`

## Best Practices

1. **Consistency**: Use CSS variables for all colors and spacing
2. **Accessibility**: Maintain WCAG AA contrast ratios (4.5:1 for text)
3. **Performance**: Use `transform` and `opacity` for animations
4. **Mobile First**: Design for small screens first
5. **Visual Hierarchy**: Use size, weight, and color to guide users
6. **Whitespace**: Give elements room to breathe

## Updated Components

The following components have been modernized with the new design system:

### Pages
- ✅ Home Page (hero, categories, featured courses)
- ✅ Courses Page (filters, course grid)
- ✅ Course Detail Page (header, lessons list)
- ✅ Login Page (auth card, form)
- ✅ Register Page (auth card, form)
- ✅ Student Dashboard (enrollments, progress)
- ✅ Instructor Dashboard (stats, course management)

### Components
- ✅ Navbar (logo, navigation links, user menu)
- ✅ Cards (course cards, enrollment cards, stat cards)
- ✅ Forms (inputs, buttons, validation)
- ✅ Empty States (no content placeholders)

## Color Psychology

- **Blue (#2563eb)**: Trust, professionalism, reliability
- **Purple (#7c3aed)**: Creativity, innovation, learning
- **Cyan (#06b6d4)**: Modern, fresh, engaging

This combination creates a professional yet approachable learning platform that inspires confidence and engagement.
