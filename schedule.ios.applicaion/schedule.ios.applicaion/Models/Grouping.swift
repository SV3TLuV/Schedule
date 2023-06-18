//
//  Grouping.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Grouping<TKey: Codable & Hashable & Equatable, TValue: Codable & Hashable & Equatable>: Codable, Hashable, Equatable {
    var key: TKey
    var items: [TValue]
}
