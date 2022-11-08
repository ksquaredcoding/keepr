export class Profile {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg ? data.coverImg : "https://images.unsplash.com/photo-1549321495-305eb13f8aa9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80"
    // TODO add additional properties if needed
  }
}

export class Account extends Profile {
  constructor(data) {
    super(data)
    this.email = data.email
  }
}