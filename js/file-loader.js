const defaultRole = {
  name: "default",
  ra_access: false,
  has_badge: false,
  display_name: "Default",
  color: "Pink",
  inheritances: [],
  permission_nodes: []
}

let input = document.getElementById("file-input")

let config

let colors = ["Pink", "Red", "Brown", "Silver", "LightGreen", "Crimson", "Cyan", "Aqua", "DeepPink", "Tomato", "Yellow", "Magenta", "BlueGreen", "Orange", "Lime", "Green", "Emerald", "Carmine", "Nickel", "Mint", "ArmyGreen", "Pumpkin"]

function getHexFromColor(color) {
  switch (color) {
    case "Pink":
      return "#FF96DE";
    case "Red":
      return "#C50000";
    case "Brown":
      return "#944710";
    case "Silver":
      return "#A0A0A0";
    case "LightGreen":
      return "#32CD32";
    case "Crimson":
      return "#DC143C";
    case "Cyan":
      return "#00B7EB";
    case "Aqua":
      return "#00FFFF";
    case "DeepPink":
      return "#FF1493";
    case "Tomato":
      return "#FF6448";
    case "Yellow":
      return "#FAFF86";
    case "Magenta":
      return "#FF0090";
    case "BlueGreen":
      return "#4DFFB8";
    case "Orange":
      return "#FF9966";
    case "Lime":
      return "#BFFF00";
    case "Green":
      return "#228B22";
    case "Emerald":
      return "#50C878";
    case "Carmine":
      return "#960018";
    case "Nickel":
      return "#727472";
    case "Mint":
      return "#98FB98";
    case "ArmyGreen":
      return "#4B5320";
    case "Pumpkin":
      return "#EE7600";
    default:
      return "pink";
  }
}

function addUser(id, roleName) {
  let columns = document.createElement("div")
  columns.classList.add("columns")

  let userColumn = document.createElement("div")
  userColumn.classList.add("column", "is-half")

  let roleColumn = document.createElement("div")
  roleColumn.classList.add("column", "is-4")

  let deleteColumn = document.createElement("div")
  deleteColumn.classList.add("column", "is-2")

  let userField = document.createElement("span")
  userField.contentEditable = true
  userField.style.padding = "0.5rem"
  userField.classList.add("has-text-success", "light-gray-outline", "rounded-corners")
  userField.innerText = id

  userField.addEventListener("input", () => {
    let temp = config.members[id]
    config.members[userField.innerText] = temp
    delete config.members[id]
    id = userField.innerText
  })

  let roleLabel = document.createElement("label")
  roleLabel.classList.add("has-text-success")
  roleLabel.htmlFor = id
  roleLabel.innerText = "Role:"
  roleLabel.style.marginRight = "1rem"

  let roleSelect = document.createElement("select")
  roleSelect.classList.add("is-medium")
  roleSelect.id = id
  for (let role of config.roles) {
    let option = document.createElement("option")
    if (roleName == role.name) option.selected = true
    option.value = role.name
    option.style.color = getHexFromColor(role.color)
    option.innerText = role.display_name
    roleSelect.appendChild(option)
  }

  roleSelect.addEventListener("change", () => {
    config.members[id] = roleSelect.value
  })

  let deleteButton = document.createElement("button")
  deleteButton.classList.add("button", "is-danger", "is-small")
  deleteButton.innerHTML = "<i class='fas fa-trash'></i>"

  deleteButton.addEventListener("click", () => {
    delete config.members[id]
    
    renderUsers()
  })

  userColumn.appendChild(userField)
  roleColumn.appendChild(roleLabel)
  roleColumn.appendChild(roleSelect)
  deleteColumn.appendChild(deleteButton)

  columns.appendChild(userColumn)
  columns.appendChild(roleColumn)
  columns.appendChild(deleteColumn)

  document.getElementById("left-column").appendChild(columns)
}

function addRole(role) {
  let div = document.createElement("div")
  div.classList.add("role-div", "mb-4")

  let displayName = document.createElement("span")
  displayName.classList.add("is-size-2")
  displayName.style.color = getHexFromColor(role.color)
  displayName.innerText = role.display_name

  div.appendChild(displayName)

  {
    let columns = document.createElement("div")
    columns.classList.add("columns")

    let leftColumn = document.createElement("div")
    leftColumn.classList.add("column", "is-3")

    let rightColumn = document.createElement("div")
    rightColumn.classList.add("column", "is-9")

    let label = document.createElement("label")
    label.classList.add("has-text-success")
    label.htmlFor = role.name + "-name"
    label.innerText = "Name:"
    label.style.marginRight = "1rem";

    let input = document.createElement("input")
    input.id = role.name + "-name"
    input.type = "text"
    input.classList.add("change-input")
    input.value = role.name

    input.addEventListener("input", () => {
      role.name = input.value
    })

    leftColumn.appendChild(label)
    rightColumn.appendChild(input)
    columns.appendChild(leftColumn)
    columns.appendChild(rightColumn)
    div.appendChild(columns)
  }

  {
    let columns = document.createElement("div")
    columns.classList.add("columns")

    let leftColumn = document.createElement("div")
    leftColumn.classList.add("column", "is-3")

    let rightColumn = document.createElement("div")
    rightColumn.classList.add("column", "is-9")

    let label = document.createElement("label")
    label.classList.add("has-text-success")
    label.htmlFor = role.name + "-ra-access"
    label.innerText = "RA Access:"
    label.style.marginRight = "1rem";

    let input = document.createElement("input")
    input.id = role.name + "-ra-access"
    input.type = "checkbox"
    input.checked = role.ra_access

    input.addEventListener("change", () => {
      role.ra_access = input.checked
    })

    leftColumn.appendChild(label)
    rightColumn.appendChild(input)
    columns.appendChild(leftColumn)
    columns.appendChild(rightColumn)
    div.appendChild(columns)
  }

  {
    let columns = document.createElement("div")
    columns.classList.add("columns")

    let leftColumn = document.createElement("div")
    leftColumn.classList.add("column", "is-3")

    let rightColumn = document.createElement("div")
    rightColumn.classList.add("column", "is-9")

    let label = document.createElement("label")
    label.classList.add("has-text-success")
    label.htmlFor = role.name + "-has-badge"
    label.innerText = "Has Badge:"
    label.style.marginRight = "1rem";

    let input = document.createElement("input")
    input.id = role.name + "-has-badge"
    input.type = "checkbox"
    input.checked = role.has_badge

    input.addEventListener("change", () => {
      role.has_badge = input.checked
    })

    leftColumn.appendChild(label)
    rightColumn.appendChild(input)
    columns.appendChild(leftColumn)
    columns.appendChild(rightColumn)
    div.appendChild(columns)
  }

  {
    let columns = document.createElement("div")
    columns.classList.add("columns")

    let leftColumn = document.createElement("div")
    leftColumn.classList.add("column", "is-3")

    let rightColumn = document.createElement("div")
    rightColumn.classList.add("column", "is-9")

    let label = document.createElement("label")
    label.classList.add("has-text-success")
    label.htmlFor = role.name + "-display-name"
    label.innerText = "Display Name:"
    label.style.marginRight = "1rem";

    let input = document.createElement("input")
    input.id = role.name + "-display-name"
    input.type = "text"
    input.classList.add("change-input")
    input.value = role.display_name

    input.addEventListener("input", () => {
      displayName.innerText = input.value
      role.display_name = input.value
    })

    leftColumn.appendChild(label)
    rightColumn.appendChild(input)
    columns.appendChild(leftColumn)
    columns.appendChild(rightColumn)
    div.appendChild(columns)
  }

  {
    {
      let columns = document.createElement("div")
      columns.classList.add("columns")

      let leftColumn = document.createElement("div")
      leftColumn.classList.add("column", "is-3")

      let rightColumn = document.createElement("div")
      rightColumn.classList.add("column", "is-9")

      let label = document.createElement("label")
      label.classList.add("has-text-success")
      label.htmlFor = role.name + "-color"
      label.innerText = "Color:"
      label.style.marginRight = "1rem";

      let input = document.createElement("select")
      input.id = role.name + "-color"
      for (let color of colors) {
        let option = document.createElement("option")
        if (role.color == color) option.selected = true
        option.value = color
        option.style.backgroundColor = "#000"
        option.style.color = getHexFromColor(color)
        option.innerText = color
        input.appendChild(option)
      }

      input.addEventListener("change", () => {
        role.color = input.value
        displayName.style.color = getHexFromColor(role.color)
      })

      leftColumn.appendChild(label)
      rightColumn.appendChild(input)
      columns.appendChild(leftColumn)
      columns.appendChild(rightColumn)
      div.appendChild(columns)
    }
  }

  {
    {
      let columns = document.createElement("div")
      columns.classList.add("columns")

      let leftColumn = document.createElement("div")
      leftColumn.classList.add("column", "is-3")

      let rightColumn = document.createElement("div")
      rightColumn.classList.add("column", "is-9")

      let label = document.createElement("label")
      label.classList.add("has-text-success")
      label.htmlFor = role.name + "-inheritances"
      label.innerText = "Inheritances:"
      label.style.marginRight = "1rem";

      let input = document.createElement("select")
      input.style.maxHeight = "6rem";
      input.style.overflowY = "auto"
      input.multiple = true
      input.id = role.name + "-inheritances"
      for (let r of config.roles) {
        let option = document.createElement("option")
        if (role.inheritances.includes(r.name)) option.selected = true
        option.value = r.name
        option.style.color = getHexFromColor(r.color)
        option.innerText = r.display_name
        input.appendChild(option)
      }

      input.addEventListener("change", () => {
        role.inheritances = [...input.selectedOptions].map(o => o.value)
      })

      leftColumn.appendChild(label)
      rightColumn.appendChild(input)
      columns.appendChild(leftColumn)
      columns.appendChild(rightColumn)
      div.appendChild(columns)
    }
  }

  {
    let columns = document.createElement("div")
    columns.classList.add("columns")

    let leftColumn = document.createElement("div")
    leftColumn.classList.add("column", "is-3")

    let rightColumn = document.createElement("div")
    rightColumn.classList.add("column", "is-9")

    let label = document.createElement("label")
    label.classList.add("has-text-success")
    label.htmlFor = role.name + "-permissions"
    label.innerText = "Permissions:"
    label.style.marginRight = "1rem"

    let input = document.createElement("textarea")
    input.id = role.name + "-permissions"
    input.type = "text"
    input.classList.add("permissions-input")
    input.value = role.permission_nodes.join("\n")

    input.addEventListener("input", () => {
      role.permission_nodes = input.value.split("\n")
    })

    leftColumn.appendChild(label)
    rightColumn.appendChild(input)
    columns.appendChild(leftColumn)
    columns.appendChild(rightColumn)
    div.appendChild(columns)
  }

  {
    let upArrow = document.createElement("button")
    upArrow.classList.add("button", "is-success", "is-small")
    upArrow.innerHTML = "<i class='fas fa-arrow-up'></i>"
    upArrow.addEventListener("click", () => {
      let index = config.roles.findIndex(r => r.name == role.name)
      if (index == 0) return
      let temp = config.roles[index - 1]
      config.roles[index - 1] = role
      config.roles[index] = temp
      renderRoles()
    })

    let downArrow = document.createElement("button")
    downArrow.classList.add("button", "is-success", "is-small")
    downArrow.innerHTML = "<i class='fas fa-arrow-down'></i>"
    downArrow.addEventListener("click", () => {
      let index = config.roles.findIndex(r => r.name == role.name)
      if (index == config.roles.length - 1) return
      let temp = config.roles[index + 1]
      config.roles[index + 1] = role
      config.roles[index] = temp
      renderRoles()
    })

    let deleteButton = document.createElement("button")
    deleteButton.classList.add("button", "is-danger", "is-small")
    deleteButton.innerHTML = "<i class='fas fa-trash'></i>"
    deleteButton.addEventListener("click", () => {
      config.roles = config.roles.filter(r => r.name != role.name)
      renderRoles()
    })

    let buttons = document.createElement("div")
    buttons.classList.add("buttons")
    buttons.appendChild(upArrow)
    buttons.appendChild(downArrow)
    buttons.appendChild(deleteButton)
    div.appendChild(buttons)
  }

  document.getElementById("right-column").appendChild(div)
}

function renderUsers() {
  let leftColumn = document.getElementById("left-column")
  leftColumn.innerHTML = ""
  for (let [id, role] of Object.entries(config.members)) {
    addUser(id, role)
  }

  let addButton = document.createElement("button")
  addButton.classList.add("button", "is-success", "is-small")
  addButton.innerHTML = "<i class='fas fa-plus'></i>"
  addButton.addEventListener("click", () => {
    config.members[`${Object.keys(config.members).length}@steam`] = "default"
    renderUsers()
  })
  leftColumn.appendChild(addButton)
}

function renderRoles() {
  let rightColumn = document.getElementById("right-column")
  rightColumn.innerHTML = ""
  for (let role of config.roles) {
    addRole(role)
  }

  let addButton = document.createElement("button")
  addButton.classList.add("button", "is-success", "is-small")
  addButton.innerHTML = "<i class='fas fa-plus'></i>"
  addButton.addEventListener("click", () => {
    defaultRole.name = `role#${config.roles.length}`
    config.roles.push(defaultRole)
    renderRoles()
  })
  rightColumn.appendChild(addButton)
}

input.addEventListener("change", () => {
  let files = input.files
  if (files.length == 0) return
  const file = files[0]
  let reader = new FileReader()
  reader.onload = (e) => {
    const file = e.target.result
    try {
      const obj = jsyaml.load(file, {

      })
      document.getElementById("file-input-container").classList.add("hidden")
      document.getElementById("main-container").classList.remove("hidden")
      document.getElementById("save-button-container").classList.remove("hidden")

      config = obj

      renderUsers()

      renderRoles()

    } catch (e) { alert("File is not yml.") }
  }
  reader.onerror = (e) => alert(e.target.error.name)
  reader.readAsText(file)
})

let saveButton = document.getElementById("save-button")
saveButton.addEventListener("click", () => {
  let a = document.createElement("a")
  a.href = URL.createObjectURL(new Blob([jsyaml.dump(config)], { type: "text/yaml" }))
  a.download = "config.yml"
  a.click()
})