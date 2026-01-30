window.Olympus.passkeys = {

	create: async (optionsJson) => {

		if (!window.PublicKeyCredential || typeof PublicKeyCredential.parseCreationOptionsFromJSON !== 'function') {

			console.error("Passkeys are not supported or the browser is outdated.");

			return null;

		}

		try {

			const optionsObj = JSON.parse(optionsJson);
			const publicKey = PublicKeyCredential.parseCreationOptionsFromJSON(optionsObj);
			const credential = await navigator.credentials.create({ publicKey });

			return JSON.stringify(credential);

		} catch (error) {

			console.error("Error creating passkey:", error);

			return null;

		}

	},

	get: async (optionsJson) => {

		if (!window.PublicKeyCredential || typeof PublicKeyCredential.parseRequestOptionsFromJSON !== 'function') {

			console.error("Passkeys are not supported or the browser is outdated.");

			return null;

		}

		try {

			const optionsObj = JSON.parse(optionsJson);
			const publicKey = PublicKeyCredential.parseRequestOptionsFromJSON(optionsObj);
			const credential = await navigator.credentials.get({ publicKey });

			return JSON.stringify(credential);

		} catch (error) {

			console.error("Error obtaining passkey:", error);

			return null;

		}
	},

	isSupported: async () => {

		return window.PublicKeyCredential &&
			typeof PublicKeyCredential.parseCreationOptionsFromJSON === 'function' &&
			typeof PublicKeyCredential.parseRequestOptionsFromJSON === 'function' &&
			(await window.PublicKeyCredential.isUserVerifyingPlatformAuthenticatorAvailable());

	}

};
