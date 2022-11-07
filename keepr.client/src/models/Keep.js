export class Keep {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.creatorId = data.creatorId
    this.creator = data.creator
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.kept = data.kept
  }
}

export class VaultKept extends Keep {
  constructor(data) {
    super(data)
    this.vaultId = data.vaultId
    this.keepId = data.keepId
    this.vaultKeepId = data.vaultKeepId
  }
}