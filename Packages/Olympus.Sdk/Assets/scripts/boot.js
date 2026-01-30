window.Olympus = window.Olympus || {};

window.Olympus.themes = {

	media: window.matchMedia('(prefers-color-scheme: dark)'),

	apply: function (theme) {

		const target = theme || localStorage.getItem('theme') || 'system';
		const isDark = target === 'system' ? this.media.matches : target === 'dark';
		const dark = document.getElementById('theme-dark');

		if (dark) dark.disabled = !isDark;

		localStorage.setItem('theme', target);

	},

	init: function () {

		const light = document.getElementById('theme-light');
		const common = document.getElementById('theme-common');

		if (light) light.disabled = false;
		if (common) common.disabled = false;

		this.media.addEventListener('change', () => {

			if (localStorage.getItem('theme') === 'system') {

				this.apply('system');

			}

		});

		this.apply();

	}

};

window.Olympus.themes.init();
