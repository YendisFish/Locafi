<script>
    import axios from "axios";
    import DownloadMenu from './DownloadMenu.svelte';
    import { currentPlaylist, currentSongList } from "../globals.js";
    import Song from "./Song.svelte";

    let mode = "DEFAULT";
    let addSong = false;
    
    let songList = getPlaylistSongs();
    
    async function setAndGetPlaylist(playlist) {
        currentPlaylist.set(playlist);
        await getPlaylistSongs();
    }
    
    async function getPlaylistSongs() {
        songList = (await axios.post("http://localhost:5000/playlist/GET_PLAYLIST_SONGS", selectedPlaylist)).data;
    }
    
    async function onDownload() {
        await setAndGetPlaylist(selectedPlaylist);
        addSong = false;
    }

    // Subscribe to the store to get its current value
    let selectedPlaylist = null;

    // Subscribe to changes in the globalVariable
    currentPlaylist.subscribe(value => {
        if(value != selectedPlaylist) {
            selectedPlaylist = value;
            getPlaylistSongs()
        }
    });
</script>

<style>
    .main-page {
        background: #2b2f32;
        height: 100vh;
        width: 60vw;
        display: flex; /* Use flexbox for centering */
        flex-direction: column;
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
        width: 98.5%; /* Set width to 100% of container */
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

    .download-menu {
        padding: 8px 50px 8px 50px;
    }
</style>

<div class="main-page">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>

    {#if selectedPlaylist != null}
        <div class="download-menu">
            {#if addSong}
                <DownloadMenu onDownload="{onDownload}" />
                <button on:click={() => addSong = !addSong}>Cancel</button>
            {:else}
                <button on:click={() => addSong = !addSong} style="margin-top: 15px">Add Song</button>
            {/if}
        </div>

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
                    <Song class="song" song={song} playlist={selectedPlaylist} />
                {/each}
                </tbody>
            </table>
        </div>
    {/if}
</div>