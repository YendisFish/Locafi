<script>
    import InputComponent from "./InputComponent.svelte";
    import axios from "axios";
    import { currentPlaylist } from "../globals.js";

    export let onDownload;
    
    let linkText = "";
    let nameText = "";
    
    function onChangeInput(e) {
        linkText = e.target.value;
    }
    
    function onChangeName(e) {
        nameText = e.target.value;
    }

    async function download() {
        let res = await axios.post("http://localhost:5000/DOWNLOAD", { link: linkText, playlist: selectedPlaylist, name: nameText });
        await onDownload();
    }

    let selectedPlaylist = null;

    // Subscribe to changes in the globalVariable
    currentPlaylist.subscribe(value => {
        selectedPlaylist = value;
    });
</script>

<style>

</style>

<div class="download-menu">
    <p style="color:white">Dowload</p>
    <InputComponent onChange="{onChangeInput}"/>
    <p style="color:white">Name</p>
    <InputComponent onChange="{onChangeName}"/>
    <button on:click={download}>Download</button>
</div>