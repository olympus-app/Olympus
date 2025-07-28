window.themeManager = {

	keys: {
		style: 'style',
		schema: 'schema',
		palette: 'palette',
	},

	schemas: {
		common: 'common',
		system: 'system',
		light: 'light',
		dark: 'dark',
	},

	defaults: {
		style: 'bootstrap',
		schema: 'system',
		palette: 'default',
	},

	elements: {
		color: document.querySelector('meta[name="theme-color"]'),
		light: document.getElementById('theme-light'),
		dark: document.getElementById('theme-dark'),
		common: document.getElementById('theme-common'),
		palette: document.getElementById('theme-palette'),
	},

	applyStyle: function (style) {

		style ??= localStorage.getItem(this.keys.style) || this.defaults.style;

		if (this.elements.light && !this.elements.light.href.match(style)) this.elements.light.href = `styles/${style}/light.css`;
		if (this.elements.dark && !this.elements.dark.href.match(style)) this.elements.dark.href = `styles/${style}/dark.css`;
		if (this.elements.common && !this.elements.common.href.match(style)) this.elements.common.href = `styles/${style}/common.css`;

		localStorage.setItem(this.keys.style, style);

		this.setThemeColor();

	},

	applySchema: function (schema) {

		schema ??= localStorage.getItem(this.keys.schema) || this.defaults.schema;
		system = window.matchMedia('(prefers-color-scheme: dark)').matches ? this.schemas.dark : this.schemas.light;
		isDark = schema === this.schemas.system ? system === this.schemas.dark : schema === this.schemas.dark;

		if (this.elements.dark && this.elements.dark.disabled !== !isDark) this.elements.dark.disabled = !isDark;

		localStorage.setItem(this.keys.schema, schema);

		this.setThemeColor();

	},

	applyPalette: function (palette) {

		palette ??= localStorage.getItem(this.keys.palette) || this.defaults.palette;

		if (this.elements.palette && !this.elements.palette.href.match(palette)) this.elements.palette.href = `styles/palettes/${palette}.css`;

		localStorage.setItem(this.keys.palette, palette);

		this.setThemeColor();

	},

	setThemeColor: function () {

		setTimeout(() => {

			const color = getComputedStyle(document.documentElement).getPropertyValue('--rz-header-background-color').trim();

			if (color && this.elements.color) this.elements.color.setAttribute('content', color);

		}, 100);

	},

	clearDupes: function () {

		const selectors = ['#theme-light', '#theme-dark', '#theme-common', '#theme-palette'];

		selectors.forEach(selector => {
			const elements = document.querySelectorAll(selector);
			for (let i = 1; i < elements.length; i++) { elements[i].remove(); }
		});

		this.elements = {
			color: document.getElementById('theme-color'),
			light: document.getElementById('theme-light'),
			dark: document.getElementById('theme-dark'),
			common: document.getElementById('theme-common'),
			palette: document.getElementById('theme-palette'),
		};

	},

	addListener: function () {

		const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
		mediaQuery.addEventListener('change', e => this.applySchema());

	},

	init: function () {

		this.applyStyle();
		this.applySchema();
		this.applyPalette();
		this.addListener();

	}

};

window.themeManager.init();
