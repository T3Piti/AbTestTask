var usersData = [];

const tableStyle = {
    fontFamily: "Ubuntu",
    fontStyle: "normal",
    fontWeight: "normal",
    fontSize: "14px",
    lineHeight: "16px",
    color: "#5D6D97",
    width: "770px",
    height: "48px",
    left: "50%",
    textAlign: "center"
};
const thStyle = {
    padding: "5px"
};
const tdStyle = {
    padding: "5px",
    margin: "5px"
};
const trStyle = {
    margin: "5px",
    borderBottom: "1px solid black"
};
const divStyle = {
    flexDirection: "column"
};
const buttonStyle = {
    float: "left",
    borderRadius: "10px",
    background: "#4A9DFF",
    width: "189px",
    height: "38px",
    fontFamily: "Ubuntu",
    fontStyle: "normal",
    fontWeight: "normal",
    fontSize: "14px",
    lineHeight: "16px",
    color: "#FFFFFF",
    margin: "5px",
    border: "none"
};

const apiUrl = "/api/users";
var ctx = document.getElementById('chartCanv');

function formatDate(date, isDDMMYYYY) {

    if (isDDMMYYYY === true) {
        var dd = date.getDate();
        if (dd < 10) dd = '0' + dd;

        var mm = date.getMonth() + 1;
        if (mm < 10) mm = '0' + mm;

        var yy = date.getFullYear();
        return dd + '.' + mm + '.' + yy;
    } else {
        var dd = date.substring(0, 2)+"T00:00:00";
        var mm = date.substring(3, 5);
        var yy = date.substring(6,10);
        return yy + '-' + mm + '-' + dd
    }
}

function tableToJson(table) {
    var data = [];

    var headers = ['id', 'dateRegistrate', 'dateLastActive'];

    for (var i = 1; i < table.rows.length; i++) {

        var tableRow = table.rows[i];
        var rowData = {};

        for (var j = 0; j < tableRow.cells.length; j++) {

            rowData[headers[j]] = tableRow.cells[j].innerHTML;

        }
        data.push(rowData);
    }
    return data;
}

class User extends React.Component {
    constructor(props) {
        super(props);
        this.props.user.dateLastActive = formatDate(new Date(this.props.user.dateLastActive.substring(0, 10)), true);
        this.props.user.dateRegistrate = formatDate(new Date(this.props.user.dateRegistrate.substring(0, 10)), true);
    }
    render() {
        return (
            <tr key={this.props.user.id}>
                <td style={tdStyle}>{this.props.user.id}</td>
                <td style={tdStyle}
                    contentEditable value={this.props.user.dateRegistrate}>{this.props.user.dateRegistrate}</td>
                <td style={tdStyle}
                    contentEditable value={this.props.user.dateLastActive}>{this.props.user.dateLastActive}</td>
            </tr>
            )
    }
}

class Table extends React.Component {
    constructor(props) {
        super(props);
        this.state = { users: props.users };
    }

    render() {
        return (
                <table style={tableStyle} id="UsersTable">
                    <thead>
                        <tr>
                            <th style={thStyle}>Id</th>
                            <th style={thStyle}>Registration Date</th>
                            <th style={thStyle}>Date Last Activity</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.users.map(
                            item => (
                                <User user={item} />
                            )
                        )}
                    </tbody>
                </table>
        );
    }
}

const onSaveButtonClick = function (e) {
    usersData = tableToJson(document.getElementById("UsersTable"));
    SaveTable(usersData);
}

function SaveTable(jsonData) {
    var url = apiUrl + "/SaveEdits";
    jsonData.map(
        (item) => (
            item.dateLastActive = formatDate(item.dateLastActive, false),
            item.dateRegistrate = formatDate(item.dateRegistrate, false)
        ))
    const fetchOptions = {
        method: 'Post',
        headers: new Headers({
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(jsonData)
    };

    fetch(url, fetchOptions).then(res => {
        if (res.ok) {
            alert("Данные сохранены");
            return res.json()
        }
        return res.json().then(error => {
            alert(JSON.stringify(error.title));
        })
    });
    
}
    
class SaveButton extends React.Component {
    render() {

        return (
            <input type="button"
                placeholder="Цена"
                value='Save'
                onClick={onSaveButtonClick}
                style={buttonStyle}/>
            )
    }
}

class TableForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: []
        };
    }

    componentDidMount() {
        fetch(apiUrl).then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }
    render() {
        const { error, isLoaded, items } = this.state;
        usersData = JSON.stringify(items);
        if (error) {
            return <p>Ошибка</p>
        } else if (!isLoaded) {
            return <p>Loading</p>
        } else {
            return (
                <div>
                    <Table users={items} />
                    <SaveButton />
                </div>
            )
        }
    }
}

ReactDOM.render(
        <TableForm />,
    document.getElementById("content")
);