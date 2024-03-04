<script>
    import axios from "axios";
    import { currentPlaylist } from "../globals.js";

    export let playlists = [];
    getPlaylists();

    let playlistNameInput = "";
    let showCreatePlaylist = false;
    
    async function getPlaylists() {
        playlists = (await axios.get("http://localhost:5000/playlist/GET_PLAYLIST_LIST")).data;
    }
    
    async function createPlaylist() {
        let res = await axios.post("http://localhost:5000/playlist/CREATE_PLAYLIST", { name: playlistNameInput });
        await toggleShowPlaylist();
        await getPlaylists();
    }
    
    async function onCreateChange(e) {
        playlistNameInput = e.target.value;
    }
    
    async function toggleShowPlaylist() {
        showCreatePlaylist = !showCreatePlaylist;
    }
    
    async function selectPlaylist(playlist) {
        currentPlaylist.set(playlist);
    }
</script>

<style>
    .playlist-tab {
        background: #17191a;
        height: 100vh;
        width: 20vw;
    }

    .page-table {
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen-Sans, Ubuntu, Cantarell, "Helvetica Neue", sans-serif;
        color: white;
    }
    
    table {
        border-collapse: collapse;
        width: 98.5%;
    }
    
    th {
        padding: 8px 50px 8px 50px;
        text-align: left;
    }
    
    td {
        padding: 8px 50px 8px 50px;
        text-align: left;
    }

    table, th, td {
        border: none;
    }
    
    .playlist-element {
        width: 17vw;
        padding: 2% 2% 2% 5%;
        overflow: hidden;
        white-space: nowrap;
    }
    
    .create-playlist {
        padding: 8px;
        color: white;
    }
    
    .create-playlist-button {
        margin: 8px;
    }
</style>

<div class="playlist-tab">
    {#if showCreatePlaylist}
        <div class="create-playlist">
            <p>Enter Name:</p>
            <input on:change={onCreateChange} type="text" />
            <button on:click={createPlaylist}>Create</button>
            <button on:click={toggleShowPlaylist}>Cancel</button>
        </div>
    {:else}
        <button on:click={toggleShowPlaylist} class="create-playlist-button">Create Playlist</button>
    {/if}
    
    <table class="page-table">
        {#each playlists as playlist}
            <tr>
                <div class="playlist-element" on:click={async () => await selectPlaylist(playlist)}>
                    <p>{playlist.name}</p>
                </div>
            </tr>
        {/each}
    </table>
</div>