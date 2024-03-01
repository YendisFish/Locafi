<script>
    export let text = "";
    export let onChange;
</script>

<style>
    .input-component {
        z-index: 10;
    }
</style>

<div class="input-component">
    <input type="text" on:change={onChange} class="input-element" />
</div>