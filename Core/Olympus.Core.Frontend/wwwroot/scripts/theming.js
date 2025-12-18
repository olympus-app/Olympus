window.themeManager = {

	keys: {
		style: 'style',
		colors: 'colors',
		schema: 'schema',
	},

	schemas: {
		common: 'common',
		system: 'system',
		light: 'light',
		dark: 'dark',
	},

	defaults: {
		style: 'default',
		colors: 'default',
		schema: 'system',
	},

	elements: {
		theme_color: document.querySelector('meta[name="theme-color"]'),
		theme_light_base: document.getElementById('theme-light-base'),
		theme_light_custom: document.getElementById('theme-light-custom'),
		theme_dark_base: document.getElementById('theme-dark-base'),
		theme_dark_custom: document.getElementById('theme-dark-custom'),
		theme_common: document.getElementById('theme-common'),
		theme_colors: document.getElementById('theme-colors'),
	},

	applyColor: function () {

		const update = () => {
			const color = getComputedStyle(document.documentElement).getPropertyValue('--rz-header-background-color').trim();
			if (color && this.elements.theme_color) this.elements.theme_color.setAttribute('content', color);
		};

		setTimeout(() => { update(); }, 50);
		setTimeout(() => { update(); }, 100);
		setTimeout(() => { update(); }, 250);
		setTimeout(() => { update(); }, 500);
		setTimeout(() => { update(); }, 1000);
		setTimeout(() => { update(); }, 2000);
		setTimeout(() => { update(); }, 3000);

	},

	applyStyle: function (style) {

		style ??= localStorage.getItem(this.keys.style);
		if (!style) return;

		if (this.elements.theme_light_base && !this.elements.theme_light_base.href.match(style)) this.elements.theme_light_base.href = `_content/Olympus.Frontend.Interface/themes/${style}/light-base.css`;
		if (this.elements.theme_light_custom && !this.elements.theme_light_custom.href.match(style)) this.elements.theme_light_custom.href = `_content/Olympus.Frontend.Interface/themes/${style}/light-custom.css`;
		if (this.elements.theme_dark_base && !this.elements.theme_dark_base.href.match(style)) this.elements.theme_dark_base.href = `_content/Olympus.Frontend.Interface/themes/${style}/dark-base.css`;
		if (this.elements.theme_dark_custom && !this.elements.theme_dark_custom.href.match(style)) this.elements.theme_dark_custom.href = `_content/Olympus.Frontend.Interface/themes/${style}/dark-custom.css`;
		if (this.elements.theme_common && !this.elements.theme_common.href.match(style)) this.elements.theme_common.href = `_content/Olympus.Frontend.Interface/themes/${style}/common.css`;

		localStorage.setItem(this.keys.style, style);

		this.applyColor();

	},

	applyColors: function (colors) {

		colors ??= localStorage.getItem(this.keys.colors);
		if (!colors) return;

		if (this.elements.theme_colors && !this.elements.theme_colors.href.match(colors)) this.elements.theme_colors.href = `_content/Olympus.Frontend.Interface/themes/colors/${colors}.css`;

		localStorage.setItem(this.keys.colors, colors);

		this.applyColor();

	},

	applySchema: function (schema) {

		schema ??= localStorage.getItem(this.keys.schema);
		if (!schema) return;

		system = window.matchMedia('(prefers-color-scheme: dark)').matches ? this.schemas.dark : this.schemas.light;
		isDark = schema === this.schemas.system ? system === this.schemas.dark : schema === this.schemas.dark;

		if (this.elements.theme_dark_base && this.elements.theme_dark_base.disabled !== !isDark) this.elements.theme_dark_base.disabled = !isDark;
		if (this.elements.theme_dark_custom && this.elements.theme_dark_custom.disabled !== !isDark) this.elements.theme_dark_custom.disabled = !isDark;

		localStorage.setItem(this.keys.schema, schema);

		this.applyColor();

	},

	addListener: function () {

		const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
		mediaQuery.addEventListener('change', event => this.applySchema());

	},

	init: function () {

		this.applyStyle();
		this.applySchema();
		this.applyColors();
		this.addListener();

	}

};

window.themeManager.init();
