import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/VaultKept.js').VaultKept[]} */
  vaultkeeps: [],
  /** @type {import('./models/Keep.js').Keep} */
  activeKeep: null,
  /** @type {import('./models/Keep.js').Keep[]} */
  profileKeeps: [],
  activeVault: null,
  /** @type {import('./models/Profile.js').Profile} */
  activeProfile: null,
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],
})
