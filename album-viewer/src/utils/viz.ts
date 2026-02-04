// generate a plot with D3.js of the selling price of the album by year
// x-axis are the month series and y-axis show the numbers of albums sold
// data from the sales of album are loaded in from an external source and are in json format

import * as d3 from "d3";

interface AlbumSalesData {
    month: string; // e.g. "January", "February", etc.
    albumsSold: number;
}

export function generateAlbumSalesPlot(data: AlbumSalesData[], containerId: string): void {
    // Set dimensions and margins for the plot
    const margin = { top: 20, right: 30, bottom: 40, left: 50 };
    const width = 800 - margin.left - margin.right;
    const height = 400 - margin.top - margin.bottom;
    
    // Create SVG container

    const svg = d3.select(`#${containerId}`)
        .append('svg')
        .attr('width', width + margin.left + margin.right)
        .attr('height', height + margin.top + margin.bottom)
        .append('g')
        .attr('transform', `translate(${margin.left},${margin.top})`);
            
    // Set the scales
    const x = d3.scaleBand()
        .domain(data.map(d => d.month))
        .range([0, width])
        .padding(0.1);
    const y = d3.scaleLinear()
        .domain([0, d3.max(data, d => d.albumsSold)!])
        .nice()
        .range([height, 0]);
            
    // Add the x-axis
    svg.append('g')
        .attr('transform', `translate(0,${height})`)
        .call(d3.axisBottom(x));
        
    // Add the y-axis
    svg.append('g')
        .call(d3.axisLeft(y));
            
    // Create bars
    svg.selectAll('.bar')
        .data(data)
        .enter()
        .append('rect')
        .attr('class', 'bar')
        .attr('x', d => x(d.month)!)
        .attr('y', d => y(d.albumsSold))
        .attr('width', x.bandwidth())
        .attr('height', d => height - y(d.albumsSold))
        .attr('fill', 'steelblue');
}
