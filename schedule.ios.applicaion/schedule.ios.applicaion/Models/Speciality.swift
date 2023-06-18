//
//  Speciality.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Speciality: Identifiable, Codable, Equatable, Hashable {
    var id: Int
    var code: String
    var name: String
    var maxTermId: Int
    var isDeleted: Bool
}
