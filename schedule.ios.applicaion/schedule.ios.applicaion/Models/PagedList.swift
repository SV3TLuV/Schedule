//
//  PagedList.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 12.06.2023.
//

import Foundation

struct PagedList<TItem: Codable> : Codable {
    var pageSize: Int
    var pageNumber: Int
    var totalCount: Int
    var totalPages: Int
    var items: [TItem]
}
