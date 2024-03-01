<script>
    let currentPlaylist = "";
    let songList = [ { name:"Some Song", path:"./someplaylist" }, { name:"Other Song", path:"./someplaylist"} ];
    
    function setAndGetPlaylist(playlist) {
        
        currentPlaylist = playlist;
    }
</script>

<style>
    .main-page {
        background: #2b2f32;
        height: 100vh;
        width: 60vw;
        display: flex; /* Use flexbox for centering */
        margin: 0 auto; /* Center the container horizontally */
        padding-left: 15px;
    }
    
    .page-table {
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen-Sans, Ubuntu, Cantarell, "Helvetica Neue", sans-serif;
        color: white;
    }
    
    /* Apply styles to the table */
    table {
        border-collapse: collapse; /* Collapse borders */
        width: 100%; /* Set width to 100% of container */
    }

    /* Apply styles to table headers */
    th {
        padding: 8px 50px 8px 50px; /* Add padding to header cells */
        text-align: left; /* Align text to the left */
    }

    /* Apply styles to table data cells */
    td {
        padding: 8px 50px 8px 50px; /* Add padding to header cells */
        text-align: left; /* Align text to the left */
        /*padding-left: 50px;
        padding-right: 50px;*/
    }

    /* Apply alternate background color to even rows */
    /*tr:nth-child(even) {
        background-color: #1c1f21;
    }*/

    /* Remove borders from table */
    table, th, td {
        border: none; /* Remove borders */
    }
    
    .song:nth-child(even) {
        background-color: #202123;
    }
</style>

<div class="main-page">
    <link href="./node_modules/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>

    <div>
        <table class="page-table">
            <thead>
            <tr>
                <th>Name</th>
                <th>Duration</th>
                <th>Location</th>
            </tr>
            </thead>
            <tbody>
            {#each songList as song}
                <tr class="song">
                    <td>{song.name}</td>
                    <td>0</td>
                    <td>{song.path}</td>
                </tr>
            {/each}
            </tbody>
        </table>
    </div>
</div>